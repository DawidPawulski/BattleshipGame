using GameAPI.Models.Enums;

namespace GameAPI.Models.Ships
{
    public class Submarine : Ship
    {
        public Submarine()
        {
        }
        public Submarine(FieldValues nearFieldValueName)
        {
            Name = "Submarine";
            ShipNameFieldValues = FieldValues.Submarine;
            NearFieldValuesName = nearFieldValueName;
            Size = 2;
            Health = 2;
        }
    }
}