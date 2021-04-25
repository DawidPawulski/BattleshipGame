using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers.Interfaces
{
    public interface IBoardController
    {
        public Task<ActionResult<IEnumerable<Board>>> GetBoards();
        public Task<ActionResult<Board>> GetBoard(int id);
        public Task<ActionResult> PutBoard(int id, Board board);
        public Task<ActionResult<Board>> PostBoard(Board board);
        public Task<ActionResult<Board>> DeleteBoard(int id);
    }
}