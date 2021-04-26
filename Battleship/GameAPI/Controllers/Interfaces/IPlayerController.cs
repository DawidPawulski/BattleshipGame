using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers.Interfaces
{
    public interface IPlayerController
    {
        Task<ActionResult<IEnumerable<Player>>> GetPlayers();
        Task<ActionResult<Player>> GetPlayer(int id);
        Task<ActionResult> PutPlayer(int id, Player player);
        Task<ActionResult<Player>> PostPlayer(Player player);
        Task<ActionResult<Player>> DeletePlayer(int id);
    }
}