using System;
using System.Collections.Generic;
using System.Linq;
using GameAPI.Models;
using GameAPI.Models.Enums;
using GameAPI.Models.Interfaces;

namespace GameAPI.Helpers
{
    public static class ShipPlaceHelper
    {
        private const string HorizontalOrientation = "horizontal";
        private const string VerticalOrientation = "vertical";
        private const int IncrementFieldOrderNumberWhenHorizontal = 1;
        private const int IncrementFieldOrderNumberWhenVertical = 10;
        private static readonly List<string> ShipPlacementOrientation = new List<string>{ HorizontalOrientation, 
                                                                            VerticalOrientation };
        private static readonly Random Random = new Random();
        private static int _currentShipSize;
        private static string _currentShipOrientation;
        private static bool _isShipPlaced;
        private static int _startingNumber;
        private static int _currentNumber;
        private static List<int> _fieldNumbersToPlaceShips;
        private static int _fieldsField;
        private static List<int> _allowedFieldNumbers;
        private static bool _allFieldNumbersInList;
        private static List<Ship> _playerShips;
        
        public static void PlaceShips(Player player)
        {
            var fields = player.Board.Fields;
            _playerShips = player.Ships;

            foreach (var ship in _playerShips)
            {
                _currentShipSize = ship.Size;
                _isShipPlaced = false;
                
                _currentShipOrientation = ShipPlacementOrientation[Random.Next(ShipPlacementOrientation.Count)];

                switch (_currentShipOrientation)
                {
                    case HorizontalOrientation:
                    {
                        ChooseFieldsToPlaceShip(fields, IncrementFieldOrderNumberWhenHorizontal);
                        SetOrientationForTheShip(ship, _currentShipOrientation);
                        break;
                    }
                    case VerticalOrientation:
                    {
                        ChooseFieldsToPlaceShip(fields, IncrementFieldOrderNumberWhenVertical);
                        SetOrientationForTheShip(ship, _currentShipOrientation);
                        break;
                    }
                }
                
                // Iterate through board fields and place ship on it
                // if order number is the same as the number from the generated list _fieldNumbersToPlaceShips
                SetShipOnBoardField(fields, ship);
                
                // Set all near fields to be recognized as fields next to the ship
                CalculateNearShipFields(fields, _fieldNumbersToPlaceShips, ship.NearFieldValuesName);
            }
        }

        private static void SetOrientationForTheShip(IShip ship, string currentShipOrientation)
        {
            ship.Orientation = currentShipOrientation;
        }

        private static void SetShipOnBoardField(List<Field> fields, IShip ship)
        {
            foreach (var field in fields)
            {
                foreach (var numbers in _fieldNumbersToPlaceShips)
                {
                    if (field.OrderNumber == numbers)
                    {
                        field.Value = ship.ShipNameFieldValues;
                        field.ShipId = ship.Id;
                    }
                }
            }
        }

        private static void ChooseFieldsToPlaceShip(List<Field> fields, int incrementValueDependentOnOrientation)
        {
            while (!_isShipPlaced)
            {
                _fieldNumbersToPlaceShips = new List<int>();
                _allFieldNumbersInList = true;
                _fieldsField = 1;
                _startingNumber = Random.Next(1, 101);
                FillAllowedFieldNumbers(_startingNumber, _currentShipOrientation);
                _currentNumber = _startingNumber + incrementValueDependentOnOrientation;
                _fieldNumbersToPlaceShips.Add(_startingNumber);

                AddMoreFieldNumbersIfShipSizeIsBigger(incrementValueDependentOnOrientation);

                // Validate if the chosen fields to place ships are in the range of the board
                ValidateIfAllFieldsToPlaceShipsAreAllowed(fields);

                if (_allFieldNumbersInList)
                {
                    _isShipPlaced = true;
                }
            }
        }

        private static void AddMoreFieldNumbersIfShipSizeIsBigger(int incrementValueDependentOnOrientation)
        {
            while (_fieldsField < _currentShipSize)
            {
                _fieldNumbersToPlaceShips.Add(_currentNumber);
                _startingNumber = _currentNumber;
                _currentNumber += incrementValueDependentOnOrientation;
                _fieldsField++;
            }
        }

        private static void ValidateIfAllFieldsToPlaceShipsAreAllowed(List<Field> fields)
        {
            foreach (var fieldNumber in _fieldNumbersToPlaceShips)
            {
                if (!_allowedFieldNumbers.Contains(fieldNumber))
                {
                    _allFieldNumbersInList = false;
                }

                foreach (var field in fields)
                {
                    if (field.OrderNumber == fieldNumber && field.Value != FieldValues.Empty)
                    {
                        _allFieldNumbersInList = false;
                    }
                }
            }
        }

        private static void FillAllowedFieldNumbers(int startingNumber, string orientation)
        {
            _allowedFieldNumbers = new List<int>();
            
            switch (orientation)
            {
                case HorizontalOrientation:
                    _allowedFieldNumbers = startingNumber switch
                    {
                        _ when startingNumber < 11 => Enumerable.Range(1, 10).ToList(),
                        _ when startingNumber < 21 => Enumerable.Range(11, 10).ToList(),
                        _ when startingNumber < 31 => Enumerable.Range(21, 10).ToList(),
                        _ when startingNumber < 41 => Enumerable.Range(31, 10).ToList(),
                        _ when startingNumber < 51 => Enumerable.Range(41, 10).ToList(),
                        _ when startingNumber < 61 => Enumerable.Range(51, 10).ToList(),
                        _ when startingNumber < 71 => Enumerable.Range(61, 10).ToList(),
                        _ when startingNumber < 81 => Enumerable.Range(71, 10).ToList(),
                        _ when startingNumber < 91 => Enumerable.Range(81, 10).ToList(),
                        _ when startingNumber < 101 => Enumerable.Range(91, 10).ToList(),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    break;
                case VerticalOrientation:
                {
                    var startNum = startingNumber % 10;
                    for (var i = 0; i < 10; i++)
                    {
                        _allowedFieldNumbers.Add(startNum);
                        startNum += 10;
                    }
                    break;
                }
            }
        }

        private static void CalculateNearShipFields(List<Field> fields, List<int> newShipFields, FieldValues shipName)
        {
            var fieldsToOccupy = new List<int>();

            foreach (var shipField in newShipFields)
            {
                if (shipField % 10 != 0)
                {
                    fieldsToOccupy.Add(shipField+1);
                }

                if (shipField % 10 != 1)
                {
                    fieldsToOccupy.Add(shipField-1);
                }
                fieldsToOccupy.Add(shipField-10);
                fieldsToOccupy.Add(shipField+10);
            }

            SetChosenFieldsToBeRecognizedAsNearShip(fields, shipName, fieldsToOccupy);
            
        }

        private static void SetChosenFieldsToBeRecognizedAsNearShip(List<Field> fields, FieldValues shipName, List<int> fieldsToOccupy)
        {
            foreach (var fieldToOccupy in fieldsToOccupy)
            {
                foreach (var field in fields
                    .Where(field => field.OrderNumber == fieldToOccupy && field.Value == FieldValues.Empty))
                {
                    field.Value = shipName;
                }
            }
        }
    }
}