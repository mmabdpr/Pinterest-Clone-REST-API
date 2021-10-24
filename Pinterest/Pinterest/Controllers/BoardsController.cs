using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Pinterest.Models;
using Pinterest.Helpers;

namespace Pinterest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly PinterestContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BoardsController(PinterestContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Board>>> Get()
        {
            var boards = await _context.Boards.ToListAsync();
            return Ok(boards);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Board>> Get(int id)
        {
            var board = await _context.Boards.FindAsync(id);

            if (board == null)
                return NotFound();

            return Ok(board);
        }

        [AllowAnonymous]
        [HttpGet("{id}/pins")]
        public async Task<ActionResult<IEnumerable<Pin>>> GetPins(int id)
        {
            var board = await _context.Boards.FindAsync(id);

            if (board == null)
                return NotFound();

            var pins = await _context.Pins.Where(p => p.BoardId == id)
                .Include(p => p.Image)
                .ToListAsync();

            return pins;
        }

        [HttpPost]
        public async Task<ActionResult<Board>> Post([FromBody] Board board)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var callerId = int.Parse(_httpContextAccessor.HttpContext.User.Identities.First().Name);

            var pinner = await _context.Users.Where(u => u.Id == callerId).FirstAsync();
            board.Creator = pinner;
            
            await _context.Boards.AddAsync(board);
            await _context.SaveChangesAsync();
            
            // TODO respond unique board name exception properly
            
            return CreatedAtAction(nameof(Get), new {id = board.BoardId}, board);
        }
    }
}