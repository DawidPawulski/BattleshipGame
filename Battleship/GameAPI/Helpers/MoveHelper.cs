using System;
using System.Collections.Generic;
using System.Linq;
using GameAPI.Models;
using GameAPI.Models.Enums;

namespace GameAPI.Helpers
{
    public static class MoveHelper
    {
        private const string HorizontalOrientation = "horizontal";
        private const string VerticalOrientation = "vertical";
        private const int IndexForNextNumberInRow = 1;
        private const int IndexForNextNumberInColumn = 10;
        private static List<Field> _fields;
        private static Field _hitField;
        private static List<Field> _fieldsToPickList;
        private static List<Field> _previousHits;
        private static List<FieldValues> _valuesForMissedShot;
        private static List<Ship> _playersShipList;
        private static Ship _hitShip;
        private static readonly Random Random = new Random();

        public static void MakeMove(Player player, Board board)
        {
            _fields = board.Fields;
            _playersShipList = player.Ships;

            PopulatePreviousHitsListWithFields(board);
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
                
                // Reduce ship health by 1 point
                _hitShip.Health--;
                
                CheckIfShipDestroyed(board);
                
                CheckIfPlayerDestroyedAllShipsAndWon(board);
            }
        }

        private static void PopulatePreviousHitsListWithFields(Board board)
        {
            _previousHits = new List<Field>();
            
            foreach (var previousHit in board.HitsList)
            {
                _previousHits.Add(_fields.FirstOrDefault(x => x.OrderNumber == previousHit));
            }
        }

        private static void ChooseFieldToShoot()
        {
            _fieldsToPickList = new List<Field>();
            
            switch (_previousHits.Count)
            {
                case 0:
                    _fieldsToPickList = _fields.Where(x => x.isHit == false).ToList();
                    break;
                case 1:
                    var previousHit = _previousHits[0];

                    _fieldsToPickList.Add(_fields.FirstOrDefault(x => x.OrderNumber == 
                        previousHit.OrderNumber + IndexForNextNumberInRow && x.isHit == false));
                    _fieldsToPickList.Add(_fields.FirstOrDefault(x => x.OrderNumber == 
                        previousHit.OrderNumber - IndexForNextNumberInRow && x.isHit == false));
                    _fieldsToPickList.Add(_fields.FirstOrDefault(x => x.OrderNumber == 
                        previousHit.OrderNumber + IndexForNextNumberInColumn && x.isHit == false));
                    _fieldsToPickList.Add(_fields.FirstOrDefault(x => x.OrderNumber == 
                        previousHit.OrderNumber - IndexForNextNumberInColumn && x.isHit == false));
                    break;
                default:
                {
                    AssignFieldsToPickListAccordinglyToShipOrientation(IndexForNextNumberInRow, 
                        IndexForNextNumberInColumn);
                    break;
                }
            }

            try
            {
                _fieldsToPickList.RemoveAll(x => x == null);
                _hitField = _fieldsToPickList[Random.Next(_fieldsToPickList.Count)];
            }
            catch (IndexOutOfRangeException)
            {
                _hitField = _fields.FirstOrDefault(x => x.isHit == false);
            }
        }

        private static void AssignFieldsToPickListAccordinglyToShipOrientation(int indexForNextNumberInRow, 
            int indexForNextNumberInColumn)
        {
            var previousHit = _previousHits[0];

            var currentShip = _playersShipList.FirstOrDefault(x => x.Id == previousHit.ShipId);
            var shipOrientation = currentShip?.Orientation;

            switch (shipOrientation)
            {
                case HorizontalOrientation:
                {
                    foreach (var field in _previousHits)
                    {
                        _fieldsToPickList.Add(_fields.FirstOrDefault(x =>
                            x.OrderNumber == field.OrderNumber + indexForNextNumberInRow && x.isHit == false));
                        _fieldsToPickList.Add(_fields.FirstOrDefault(x =>
                            x.OrderNumber == field.OrderNumber - indexForNextNumberInRow && x.isHit == false));
                    }

                    break;
                }
                case VerticalOrientation:
                {
                    foreach (var field in _previousHits)
                    {
                        _fieldsToPickList.Add(_fields.FirstOrDefault(x =>
                            x.OrderNumber == field.OrderNumber + indexForNextNumberInColumn && x.isHit == false));
                        _fieldsToPickList.Add(_fields.FirstOrDefault(x =>
                            x.OrderNumber == field.OrderNumber - indexForNextNumberInColumn && x.isHit == false));
                    }

                    break;
                }
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
                board.ShipNearFields.Clear();
                board.HitsList.Clear();
            }
            else
            {
                board.HitsList.Add(_hitField.OrderNumber);
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
            var hitShip = _playersShipList.FirstOrDefault(x => x.Id == _hitField.ShipId);
            var nearbyFieldsToBeMarkedAsHit = new List<Field>();

            if (hitShip == null)
            {
                throw new NullReferenceException("Ship id from the hit field was not found in players ship list");
            }

            foreach (var field in _fields.Where(field => field.ShipId == hitShip.Id))
            {
                if (field.OrderNumber % 10 != 0)
                {
                    nearbyFieldsToBeMarkedAsHit.Add(_fields.FirstOrDefault(x => x.OrderNumber == field.OrderNumber + IndexForNextNumberInRow));
                }

                if (field.OrderNumber % 10 != 1)
                {
                    nearbyFieldsToBeMarkedAsHit.Add(_fields.FirstOrDefault(x => x.OrderNumber == field.OrderNumber - IndexForNextNumberInRow));
                }
                    
                nearbyFieldsToBeMarkedAsHit.Add(_fields.FirstOrDefault(x => x.OrderNumber == field.OrderNumber + IndexForNextNumberInColumn));
                nearbyFieldsToBeMarkedAsHit.Add(_fields.FirstOrDefault(x => x.OrderNumber == field.OrderNumber - IndexForNextNumberInColumn));
            }

            nearbyFieldsToBeMarkedAsHit.RemoveAll(x => x == null);
            
            foreach (var field in _fields.Where(field => nearbyFieldsToBeMarkedAsHit.Contains(field)))
            {
                field.isHit = true;
            }
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
    }
}