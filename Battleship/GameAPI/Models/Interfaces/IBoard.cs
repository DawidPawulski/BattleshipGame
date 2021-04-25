using System.Collections.Generic;
using GameAPI.Models.Enums;

namespace GameAPI.Models.Interfaces
{
    public interface IBoard
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public List<Field> Fields { get; set; }
        public List<int> ShipNearFields { get; set; }
        public List<int> HitsList { get; set; }
        public Player Player { get; set; }
        public MoveMessages Message { get; set; }

        public void CreateBoard();
    }
}