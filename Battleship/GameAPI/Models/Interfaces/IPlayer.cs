using System.Collections.Generic;
using System.Threading;
using GameAPI.Models.Ships;

namespace GameAPI.Models.Interfaces
{
    public interface IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public List<Ship> Ships { get; set; }
        public Board Board { get; set; }
    }
}