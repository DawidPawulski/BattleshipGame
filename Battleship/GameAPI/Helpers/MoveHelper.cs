using System;
using System.Collections.Generic;
using System.Linq;
using GameAPI.Models;
using GameAPI.Models.Enums;

namespace GameAPI.Helpers
{
    public static class MoveHelper
    {
        private static List<Field> _fields;
        private static Field _hitField;
        private static List<Field> _nearHitsList;
        private static List<Field> _fieldsToPickList;
        private static List<FieldValues> _valuesForMissedShot;
        private static List<Ship> _playersShipList;
        private static Ship _hitShip;
        private static readonly Random Random = new Random();

        public static void MakeMove(Player player, Board board)
        {
            _fields = board.Fields;
            _playersShipList = player.Ships;
            _nearHitsList = new List<Field>();

            PopulateNearHitsListWithFields(board);
            ChooseFieldToShoot();
            ThrowIfHitFieldNotFound();
            
            // Picked field will be recognized as hit
            _hitField.isHit = true;

            if (CheckIfShotMissed())
            {
                _hitField.Value = FieldValues.Miss;
                board.Message = MoveMessages.Miss;
            }
            else
            {
                _hitShip = ChooseCorrectShip();
                AssignNearHitsForCurrentShot();
                
                // Reduce ship health by 1 point
                _hitShip.Health--;
                
                CheckIfShipDestroyed(board);
                
                CheckIfPlayerDestroyedAllShipsAndWon(board);
            }
        }

        private static void ChooseFieldToShoot()
        {
            try
            {
                _fieldsToPickList = _nearHitsList != null && _nearHitsList.Count > 0
                    ? _nearHitsList.Where(x => x.isHit == false).ToList()
                    : _fields.Where(x => x.isHit == false).ToList();
                _hitField = _fieldsToPickList[Random.Next(_fieldsToPickList.Count)];
            }
            catch (IndexOutOfRangeException)
            {
                _hitField = _fields.FirstOrDefault(x => x.isHit == false);
            }
        }

        private static void ThrowIfHitFieldNotFound()
        {
            if (_hitField == null)
            {
                throw new NullReferenceException($"Field with chosen order number not found");
            }
        }

        private static Ship ChooseCorrectShip()
        {
            return _playersShipList.FirstOrDefault(x => x.ShipNameFieldValues == _hitField.Value && !x.IsDestroyed);
        }
        
        private static bool CheckIfShotMissed()
        {
            _valuesForMissedShot = new List<FieldValues>()
            {
                FieldValues.Empty, FieldValues.NearBattleship, FieldValues.NearCarrier, FieldValues.NearCruiser, 
                FieldValues.NearDestroyer, FieldValues.NearSubmarine, FieldValues.NearPatrolBoat,
                FieldValues.NearTacticalBoat
            };

            return _valuesForMissedShot.Contains(_hitField.Value);
        }

        private static void CheckIfShipDestroyed(Board board)
        {
            if (_hitShip.Health == 0)
            {
                _hitShip.IsDestroyed = true;
                AssignCorrectDestroyMessage(board);
                SetAllNearFieldsToBeHit();
                board.NearHits.Clear();
            }
            else
            {
                board.NearHits = _nearHitsList.Select(x => x.OrderNumber).ToList();
                board.Message = MoveMessages.Hit;
            }
        }

        private static void AssignCorrectDestroyMessage(Board board)
        {
            board.Message = _hitShip.ShipNameFieldValues switch
            {
                FieldValues.Battleship => MoveMessages.BattleshipDestroyed,
                FieldValues.Carrier => MoveMessages.CarrierDestroyed,
                FieldValues.Cruiser => MoveMessages.CruiserDestroyed,
                FieldValues.Destroyer => MoveMessages.DestroyerDestroyed,
                FieldValues.Submarine => MoveMessages.SubmarineDestroyed,
                FieldValues.TacticalBoat => MoveMessages.TacticalBoatDestroyed,
                FieldValues.PatrolBoat => MoveMessages.PatrolBoatDestroyed,
                _ => throw new NullReferenceException("Ship field value set incorrectly and " +
                                                      "can't set move message")

            };
        }

        private static void SetAllNearFieldsToBeHit()
        {
            foreach (var field in _nearHitsList)
            {
                field.isHit = true;
            }
        }

        private static void AssignNearHitsForCurrentShot()
        {
            _nearHitsList = _fields.Where(x =>
                x.ShipId == _hitShip.Id || x.Value == _hitShip.NearFieldValuesName).ToList();
        }

        private static void CheckIfPlayerDestroyedAllShipsAndWon(Board board)
        {
            foreach (var ship in _playersShipList)
            {
                if (!ship.IsDestroyed)
                {
                    return;
                }
            }

            board.Message = MoveMessages.Win;
        }

        private static void PopulateNearHitsListWithFields(Board board)
        {
            foreach (var nearHit in board.NearHits)
            {
                _nearHitsList.Add(_fields.FirstOrDefault(x => x.OrderNumber == nearHit));
            }
        }
    }
}