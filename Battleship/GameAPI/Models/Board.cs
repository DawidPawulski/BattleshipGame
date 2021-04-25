using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using GameAPI.Models.Enums;
using GameAPI.Models.Interfaces;

namespace GameAPI.Models
{
    public class Board : IBoard
    {
        private IEnumerable<int> _boardSeq;
        private int _fieldOrderNumber = 1;
        private int _currentRow;
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Size { get; set; }

        public List<Field> Fields { get; set; }
        public List<int> NearHits { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public MoveMessages Message { get; set; }

        public void CreateBoard()
        {
            Fields = new List<Field>();
            NearHits = new List<int>();

            _boardSeq = Enumerable.Range(1, 100);

            foreach (var seq in _boardSeq)
            {
                Fields.Add(new Field(_fieldOrderNumber, _currentRow));
                _fieldOrderNumber++;
                
                if (_fieldOrderNumber < 100 && (_fieldOrderNumber-1) % 10 == 0)
                {
                    _currentRow++;
                }
            }
        }
    }
}