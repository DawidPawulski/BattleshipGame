using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameAPI.Models.Enums;
using GameAPI.Models.Interfaces;

namespace GameAPI.Models
{
    public class Field : IField
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public FieldValues Value { get; set; }
        public int OrderNumber { get; set; }
        public bool isHit { get; set; }
        public int BoardId { get; set; }
        public int ShipId { get; set; }
        public RowNames RowName { get; set; }
        public virtual Board Board { get; set; }

        public Field(int orderNumber, int rowNumber)
        {
            OrderNumber = orderNumber;
            Value = FieldValues.Empty;
            RowName = (RowNames)rowNumber;
        }
        
        public Field(int orderNumber)
        {
            OrderNumber = orderNumber;
            Value = FieldValues.Empty;
        }
    }
}