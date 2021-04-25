using GameAPI.Models.Enums;

namespace GameAPI.Models.Interfaces
{
    public interface IShip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FieldValues ShipNameFieldValues { get; set; }
        public FieldValues NearFieldValuesName { get; set; }
        public int Size { get; set; }
        public int Health { get; set; }
        public bool IsDestroyed { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}