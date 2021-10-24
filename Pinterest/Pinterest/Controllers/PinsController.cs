using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pinterest.Models;
using Pinterest.Helpers;
using Pinterest.Services;

namespace Pinterest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PinsController : ControllerBase
    {
        private readonly PinterestContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PinsController(PinterestContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Pin>> Get(int id)
        {
            var pin = await _context.Pins.FindAsync(id);

            if (pin == null)
                return NotFound();

            await _context.Entry(pin)
                .Reference(p => p.Image)
                .LoadAsync();
            
            return Ok(pin);
        }

        [HttpPost]
        public async Task<ActionResult<Pin>> Post([FromBody] Pin pin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(_httpContextAccessor.HttpContext.User.Identities.First().Name);

            var board = await _context.Boards
                .Where(b => b.CreatorId == userId && b.BoardId == pin.BoardId)
                .FirstOrDefaultAsync();
            
            if (board == null)
                return BadRequest("Board Id is not valid");

            pin.Board = board;
            
            await _context.Pins.AddAsync(pin);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = pin.PinId }, pin);
        }
    }
}