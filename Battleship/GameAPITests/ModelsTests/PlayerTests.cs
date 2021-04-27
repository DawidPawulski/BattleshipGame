using GameAPI.Models;
using NUnit.Framework;

namespace GameAPITests.ModelsTests
{
    [TestFixture]
    public class PlayerTests
    {
        private Player _player;
        
        [SetUp]
        public void Setup()
        {
            _player = new Player {Name = "Dawid"};

        }

        [Test]
        public void Player_WhenCreateNewPlayer_ShouldCreateNewPlayerShips()
        {
            const int numberOfCreatedShips = 7;
            
            Assert.AreEqual(numberOfCreatedShips, _player.Ships.Count);
        }

        [Test]
        public void Player_WhenCreateNewPlayerWithName_NamePropertyShouldReturnPlayerName()
        {
            var assignedPlayerName = "Dawid";
            
            Assert.AreEqual(assignedPlayerName, _player.Name);
        }
    }
}