using System.Threading.Tasks;
using GameAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers.Interfaces
{
    public interface IShipController
    {
        public Task<ActionResult<Board>> PutShips(int id, Player player);
    }
}