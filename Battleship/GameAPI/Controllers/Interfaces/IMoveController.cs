using System.Threading.Tasks;
using GameAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers.Interfaces
{
    public interface IMoveController
    {
        public Task<ActionResult<Board>> PutMove(int id);
    }
}