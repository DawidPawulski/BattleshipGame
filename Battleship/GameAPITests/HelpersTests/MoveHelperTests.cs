using System;
using System.Linq;
using System.Collections.Generic;
using GameAPI.Helpers;
using GameAPI.Models;
using GameAPI.Models.Enums;
using NUnit.Framework;

namespace GameAPITests.HelpersTests
{
    [TestFixture]
    public class MoveHelperTests
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
        public void MakeMove_WhenFieldListIsEmpty_ShouldThrowArgumentOutOfRangeException()
        {
            _board.Fields = new List<Field>();
            
            Assert.Throws<ArgumentOutOfRangeException>(() => MoveHelper.MakeMove(_player, _board));
        }
        
        [Test]
        public void MakeMove_WhenPlayerMakesFirstMove_OnlyOneFieldChangesIsHitToTrue()
        {
            const int numberOfFieldsChanged = 1;
            
            MoveHelper.MakeMove(_player, _board);

            var counter = _board.Fields.Count(x => x.IsHit);
            
            Assert.AreEqual(numberOfFieldsChanged, counter);
        }
        
        [Test]
        public void MakeMove_WhenPlayerMakesFirstMove_ReturnMoveMessageIsOnlyHitOrMiss()
        {
            var availableReturnMessages = new MoveMessages[] {MoveMessages.Hit, MoveMessages.Miss};
            
            MoveHelper.MakeMove(_player, _board);
            
            Assert.IsTrue(availableReturnMessages.Contains(_board.Message));
        }
    }
}