using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers.Interfaces
{
    public interface IBoardController
    {
        Task<ActionResult<IEnumerable<Board>>> GetBoards();
        Task<ActionResult<Board>> GetBoard(int id);
        Task<ActionResult> PutBoard(int id, Board board);
        Task<ActionResult<Board>> PostBoard(Board board);
        Task<ActionResult<Board>> DeleteBoard(int id);
    }
}