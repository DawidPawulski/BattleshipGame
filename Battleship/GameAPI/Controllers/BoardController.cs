using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPI.Controllers.Interfaces;
using GameAPI.Data;
using GameAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class BoardController : ControllerBase, IBoardController
    {
        private DataContext _context;

        public BoardController(DataContext context)
        {
            _context = context;
        }
        
        // GET: api/board
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Board>>> GetBoards()
        {
            return await _context.Boards.ToListAsync();
        }
        
        // GET: api/board/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Board>> GetBoard(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            
            if (board == null)
            {
                return NotFound();
            }
            
            board.Fields = await _context.Fields.Where(f => f.BoardId == board.Id).ToListAsync();

            return board;
        }
        
        // PUT: api/board/1
        [HttpPut("{id}")]
        public async Task<ActionResult> PutBoard(int id, Board board)
        {
            if (id != board.Id)
            {
                return BadRequest();
            }

            _context.Entry(board).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }
        
        // POST: api/board
        [HttpPost]
        public async Task<ActionResult<Board>> PostBoard(Board board)
        {
            board.CreateBoard();
            await _context.Boards.AddAsync(board);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoard", new {id = board.Id}, board);
        }
        
        // DELETE: api/board/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Board>> DeleteBoard(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            
            if (board == null)
            {
                return NotFound();
            }

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();

            return board;
        }

        private bool BoardExists(int id)
        {
            return _context.Boards.Any(b => b.Id == id);
        }
    }
}