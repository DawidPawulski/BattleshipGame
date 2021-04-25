using GameAPI.Models.Enums;

namespace GameAPI.Models.Ships
{
    public class Destroyer : Ship
    {
        public Destroyer()
        {
        }
        public Destroyer(FieldValues nearFieldValueName)
        {
            Name = "Destroyer";
            ShipNameFieldValues = FieldValues.Destroyer;
            NearFieldValuesName = nearFieldValueName;
            Size = 1;
            Health = 1;
        }
    }
}