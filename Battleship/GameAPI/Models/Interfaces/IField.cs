using GameAPI.Models.Enums;

namespace GameAPI.Models.Interfaces
{
    public interface IField
    {
        public int Id { get; set; }
        public FieldValues Value { get; set; }
        public int OrderNumber { get; set; }
        public bool isHit { get; set; }
        public int ShipId { get; set; }
        public RowNames RowName { get; set; }
    }
}