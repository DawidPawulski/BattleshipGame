using GameAPI.Models.Enums;

namespace GameAPI.Models.Interfaces
{
    public interface IField
    {
        int Id { get; set; }
        FieldValues Value { get; set; }
        int OrderNumber { get; set; }
        bool IsHit { get; set; }
        int ShipId { get; set; }
        RowNames RowName { get; set; }
    }
}