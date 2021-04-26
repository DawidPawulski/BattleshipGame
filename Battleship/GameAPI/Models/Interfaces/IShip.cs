using GameAPI.Models.Enums;

namespace GameAPI.Models.Interfaces
{
    public interface IShip
    {
        int Id { get; set; }
        string Name { get; set; }
        FieldValues ShipNameFieldValues { get; set; }
        FieldValues NearFieldValuesName { get; set; }
        int Size { get; set; }
        int Health { get; set; }
        bool IsDestroyed { get; set; }
        string Orientation { get; set; }
        int PlayerId { get; set; }
        Player Player { get; set; }
    }
}