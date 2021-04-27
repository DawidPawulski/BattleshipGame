using System.Linq;
using GameAPI.Helpers;
using GameAPI.Models;
using GameAPI.Models.Enums;
using NUnit.Framework;

namespace GameAPITests.HelpersTests
{
    [TestFixture]
    public class ShipPlaceHelperTests
    {
        private Player _player;
        private Board _board;
        
        [SetUp]
        public void Setup()
        {
            _player = new Player();
            _board = new Board();
            _board.CreateBoard();
            _player.Board = _board;
        }

        [Test]
        public void ShipPlaceHelper_WhenPlacingShipsOnBoard_FieldsShouldChangeValuesForAllShips()
        {
            const int numberOfFieldsTakenByShips = 18;
            var fieldValuesForShips = new[]
            {
                FieldValues.Battleship, FieldValues.Carrier, FieldValues.Cruiser, FieldValues.Destroyer,
                FieldValues.Submarine, FieldValues.PatrolBoat, FieldValues.TacticalBoat
            };
            
            ShipPlaceHelper.PlaceShips(_player);

            var counter = _board.Fields.Count(field => fieldValuesForShips.Contains(field.Value));

            Assert.AreEqual(numberOfFieldsTakenByShips, counter);
        }
        
        [Test]
        public void ShipPlaceHelper_WhenPlacingShipsOnBoard_FourFieldsShouldHaveValueBattleship()
        {
            const int numberOfFieldsTakenByBattleship = 4;
            const FieldValues fieldValueForBattleship = FieldValues.Battleship;

            ShipPlaceHelper.PlaceShips(_player);

            var counter = _board.Fields.Count(field => field.Value == fieldValueForBattleship);

            Assert.AreEqual(numberOfFieldsTakenByBattleship, counter);
        }
        
        [Test]
        public void ShipPlaceHelper_WhenPlacingShipsOnBoard_AllIsHitPropertiesShouldBeSetToFalse()
        {
            var allIsHitPropertiesAreSetToFalse = true;
            
            ShipPlaceHelper.PlaceShips(_player);

            foreach (var field in _board.Fields.Where(field => field.IsHit))
            {
                allIsHitPropertiesAreSetToFalse = false;
            }
            
            Assert.True(allIsHitPropertiesAreSetToFalse);
        }
    }
}