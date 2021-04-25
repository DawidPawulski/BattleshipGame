using GameAPI.Models.Enums;

namespace GameAPI.Models.Ships
{
    public class TacticalBoat : Ship
    {
        public TacticalBoat()
        {
        }
        public TacticalBoat(FieldValues nearFieldValueName)
        {
            Name = "Tactical boat";
            ShipNameFieldValues = FieldValues.TacticalBoat;
            NearFieldValuesName = nearFieldValueName;
            Size = 1;
            Health = 1;
        }
    }
}