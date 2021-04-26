using System.Collections.Generic;
using System.Threading;
using GameAPI.Models.Ships;

namespace GameAPI.Models.Interfaces
{
    public interface IPlayer
    {
        int Id { get; set; }
        string Name { get; set; }
        string Photo { get; set; }
        List<Ship> Ships { get; set; }
        Board Board { get; set; }
    }
}