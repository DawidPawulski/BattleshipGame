using GameAPI.Models.Enums;

namespace GameAPI.Models.Ships
{
    public class PatrolBoat : Ship
    {
        public PatrolBoat()
        {
        }
        public PatrolBoat(FieldValues nearFieldValueName)
        {
            Name = "Patrol boat";
            ShipNameFieldValues = FieldValues.PatrolBoat;
            NearFieldValuesName = nearFieldValueName;
            Size = 2;
            Health = 2;
        }
    }
}