using GameAPI.Models.Enums;

namespace GameAPI.Models.Ships
{
    public class Cruiser : Ship
    {
        public Cruiser()
        {
        }
        public Cruiser(FieldValues nearFieldValueName)
        {
            Name = "Cruiser";
            ShipNameFieldValues = FieldValues.Cruiser;
            NearFieldValuesName = nearFieldValueName;
            Size = 3;
            Health = 3;
        }
    }
}