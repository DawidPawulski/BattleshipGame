using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers.Interfaces
{
    public interface IPlayerController
    {
        public Task<ActionResult<IEnumerable<Player>>> GetPlayers();
        public Task<ActionResult<Player>> GetPlayer(int id);
        public Task<ActionResult> PutPlayer(int id, Player player);
        public Task<ActionResult<Player>> PostPlayer(Player player);
        public Task<ActionResult<Player>> DeletePlayer(int id);
    }
}