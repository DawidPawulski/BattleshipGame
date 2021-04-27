using System.Linq;
using GameAPI.Models;
using GameAPI.Models.Enums;
using NUnit.Framework;

namespace GameAPITests.ModelsTests
{
    [TestFixture]
    public class BoardTests
    {
        private Board _board;

        [SetUp]
        public void Setup()
        {
            _board = new Board();
            _board.CreateBoard();
        }

        [Test]
        public void Board_WhenCallingCreateBoardMethod_ShouldCreateListWithFields()
        {
            const int numberOfFieldsAssignedToBoard = 100;
            int counter = 0;
            
            _board.Fields.ForEach(x => counter++);
            
            Assert.AreEqual(numberOfFieldsAssignedToBoard, counter);
        }

        [Test]
        public void Board_WhenCreatingNewBoard_FieldsFrom1To10ShouldBeInFirstRow()
        {
            var isEveryFieldInFirstRow = true;
            var nameForFirstRow = RowNames.ARow;
            var orderNumbersForFirstRow = Enumerable.Range(1, 10).ToArray();

            foreach (var field in _board.Fields)
            {
                if (orderNumbersForFirstRow.Contains(field.OrderNumber))
                {
                    if (field.RowName != nameForFirstRow)
                    {
                        isEveryFieldInFirstRow = false;
                    }
                }
            }
            Assert.True(isEveryFieldInFirstRow);
        }
    }
}