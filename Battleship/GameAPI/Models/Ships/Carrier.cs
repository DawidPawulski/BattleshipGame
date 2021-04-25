using GameAPI.Models.Enums;

namespace GameAPI.Models.Ships
{
    public class Carrier : Ship
    {
        public Carrier()
        {
        }
        public Carrier(FieldValues nearFieldValueName)
        {
            Name = "Carrier";
            ShipNameFieldValues = FieldValues.Carrier;
            NearFieldValuesName = nearFieldValueName;
            Size = 5;
            Health = 5;
        }
    }
}