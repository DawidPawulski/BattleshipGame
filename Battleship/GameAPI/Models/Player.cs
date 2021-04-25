using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameAPI.Models.Enums;
using GameAPI.Models.Interfaces;
using GameAPI.Models.Ships;

namespace GameAPI.Models
{
    public class Player : IPlayer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public List<Ship> Ships { get; set; }
        public Board Board { get; set; }

        public Player()
        {
            Ships = new List<Ship>
            {
                new Battleship(FieldValues.NearBattleship),
                new Carrier(FieldValues.NearCarrier),
                new Cruiser(FieldValues.NearCruiser),
                new Destroyer(FieldValues.NearDestroyer),
                new Submarine(FieldValues.NearSubmarine),
                new PatrolBoat(FieldValues.NearPatrolBoat),
                new TacticalBoat(FieldValues.NearTacticalBoat)
            };
        }
    }
}