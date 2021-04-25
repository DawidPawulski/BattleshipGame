using System;
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
    public class MoveController : ControllerBase, IMoveController
    {
        private DataContext _context;
        private static object _syncLock = new Object();

        public MoveController(DataContext context)
        {
            _context = context;
        }
        
        // PUT: api/move/1
        // Parameter id is opponent player id
        [HttpPut("{id}")]
        [Route("api/[controller]/{id}")]
        public async Task<ActionResult<Board>> PutMove(int id)
        {
            lock (_syncLock)
            {
                var player = _context.Players.Find(id);
                var board = _context.Boards.FirstOrDefault(b => b.PlayerId == player.Id);

                if (board == null)
                {
                    return BadRequest();
                }
            
                board.Fields = _context.Fields.Where(f => f.BoardId == board.Id).ToList();
                player.Ships = _context.Ships.Where(s => s.PlayerId == player.Id).ToList();
                player.Board = board;

                if (id != player.Id)
                {
                    return BadRequest();
                }

                // Choose field on the board to hit
                MoveHelper.MakeMove(player, board);

                foreach (var field in board.Fields)
                {
                    _context.Entry(field).State = EntityState.Modified;
                }

                foreach (var ship in player.Ships)
                {
                    _context.Entry(ship).State = EntityState.Modified;
                }

                _context.Entry(board).State = EntityState.Modified;
            
                try
                {
                    _context.SaveChanges();
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
        }
        
        private bool PlayerExists(int id)
        {
            return _context.Players.Any(p => p.Id == id);
        }
    }
}