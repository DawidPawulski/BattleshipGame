using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameAPI.Models.Enums;
using GameAPI.Models.Interfaces;

namespace GameAPI.Models
{
    public abstract class Ship : IShip
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public FieldValues ShipNameFieldValues { get; set; }
        public FieldValues NearFieldValuesName { get; set; }
        public int Size { get; set; }
        public int Health { get; set; }
        public bool IsDestroyed { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}