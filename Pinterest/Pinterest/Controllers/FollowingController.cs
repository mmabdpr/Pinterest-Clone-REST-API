using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pinterest.Models;
using Pinterest.Helpers;

namespace Pinterest.Controllers
{
    [Authorize]
    [Route("api/me/[Controller]")]
    [ApiController]
    public class FollowingController : ControllerBase
    {
        private readonly PinterestContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FollowingController(PinterestContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("boards/{id}")]
        public async Task<ActionResult> FollowBoard(int id)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.Identities.First().Name);

            var isMyBoard = await _context.Boards.Where(b => b.BoardId == id && b.CreatorId == userId).AnyAsync();
            if (isMyBoard)
                return BadRequest("This is your own board! Can't follow it...");

            await _context.Followings.AddAsync(new Following(userId, id));
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("boards")]
        public async Task<ActionResult<IEnumerable<Board>>> GetFollowingBoards()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.Identities.First().Name);
            var boards = await _context.Boards.Where(b => b.Followings.Any(f => f.FollowerId == userId)).ToListAsync();
            return Ok(boards);
        }
    }
}