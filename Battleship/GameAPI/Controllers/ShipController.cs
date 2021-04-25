using System.Linq;
using System.Threading.Tasks;
using GameAPI.Controllers.Interfaces;
using GameAPI.Data;
using GameAPI.Helpers;
using GameAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class ShipController : ControllerBase, IShipController
    {
        private DataContext _context;

        public ShipController(DataContext context)
        {
            _context = context;
        }
        
        // PUT: api/ship/1/place-ships
        // Parameter id is opponent BoardId
        [HttpPut("{id}")]
        [Route("api/[controller]/{id}/place-ships")]
        public async Task<ActionResult<Board>> PutShips([FromRoute] int id, [FromBody] Player player)
        {
            var board = await _context.Boards.FindAsync(id);
            player = await _context.Players.FindAsync(board.PlayerId);
            player.Board = board;
            board.Fields = await _context.Fields.Where(f => f.BoardId == board.Id).ToListAsync();
            player.Ships = await _context.Ships.Where(s => s.PlayerId == player.Id).ToListAsync();
            
            if (id != board.Id)
            {
                return BadRequest();
            }

            // Select fields to place ship
            ShipPlaceHelper.PlaceShips(player);

            _context.Entry(player).State = EntityState.Modified;
            _context.Entry(board).State = EntityState.Modified;
            
            foreach (var field in board.Fields)
            {
                _context.Entry(field).State = EntityState.Modified;
            }

            foreach (var ship in player.Ships)
            {
                _context.Entry(ship).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return board;
        }
        
        private bool PlayerExists(int id)
        {
            return _context.Players.Any(p => p.Id == id);
        }
    }
}