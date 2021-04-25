using System.Collections.Generic;
using GameAPI.Models.Enums;

namespace GameAPI.Models.Interfaces
{
    public interface IBoard
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public List<Field> Fields { get; set; }
        public List<int> NearHits { get; set; }
        //public int PlayerId { get; set; }
        public Player Player { get; set; }
        public MoveMessages Message { get; set; }

        public void CreateBoard();
    }
}