using System.Collections.Generic;
using GameAPI.Models.Enums;

namespace GameAPI.Models.Interfaces
{
    public interface IBoard
    {
        int Id { get; set; }
        int Size { get; set; }
        List<Field> Fields { get; set; }
        List<int> ShipNearFields { get; set; }
        List<int> HitsList { get; set; }
        Player Player { get; set; }
        MoveMessages Message { get; set; }
        
        void CreateBoard();
    }
}