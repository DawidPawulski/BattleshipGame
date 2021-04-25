using GameAPI.Models.Enums;

namespace GameAPI.Models.Ships
{
    public class Battleship : Ship
    {
        public Battleship()
        {
        }
        public Battleship(FieldValues nearFieldValueName)
        {
            Name = "Battleship";
            ShipNameFieldValues = FieldValues.Battleship;
            NearFieldValuesName = nearFieldValueName;
            Size = 4;
            Health = 4;
        }
    }
}