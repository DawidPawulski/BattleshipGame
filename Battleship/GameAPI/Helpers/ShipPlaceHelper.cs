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
            const int boardSize = 100;
            const int firstField = 1;
            
            while (!_isShipPlaced)
            {
                _fieldNumbersToPlaceShips = new List<int>();
                _allFieldNumbersInList = true;
                _fieldsField = firstField;
                _startingNumber = Random.Next(firstField, boardSize+1);
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
            const int rowLength = 10;
            const int numberInTheSameColumnNextRow = 10;
            _allowedFieldNumbers = new List<int>();
            
            switch (orientation)
            {
                case HorizontalOrientation:
                    _allowedFieldNumbers = startingNumber switch
                    {
                        _ when startingNumber < 11 => Enumerable.Range(1, rowLength).ToList(),
                        _ when startingNumber < 21 => Enumerable.Range(11, rowLength).ToList(),
                        _ when startingNumber < 31 => Enumerable.Range(21, rowLength).ToList(),
                        _ when startingNumber < 41 => Enumerable.Range(31, rowLength).ToList(),
                        _ when startingNumber < 51 => Enumerable.Range(41, rowLength).ToList(),
                        _ when startingNumber < 61 => Enumerable.Range(51, rowLength).ToList(),
                        _ when startingNumber < 71 => Enumerable.Range(61, rowLength).ToList(),
                        _ when startingNumber < 81 => Enumerable.Range(71, rowLength).ToList(),
                        _ when startingNumber < 91 => Enumerable.Range(81, rowLength).ToList(),
                        _ when startingNumber < 101 => Enumerable.Range(91, rowLength).ToList(),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    break;
                case VerticalOrientation:
                {
                    var startNum = startingNumber % rowLength;
                    for (var i = 0; i < rowLength; i++)
                    {
                        _allowedFieldNumbers.Add(startNum);
                        startNum += numberInTheSameColumnNextRow;
                    }
                    break;
                }
            }
        }

        private static void CalculateNearShipFields(List<Field> fields, List<int> newShipFields, FieldValues shipName)
        {
            const int rowLength = 10;
            const int rightEdgeOfTheRow = 0;
            const int leftEdgeOfTheRow = 1;
            const int indexForNextNumberInRow = 1;
            const int indexForNextNumberInColumn = 10;
            var fieldsToOccupy = new List<int>();

            foreach (var shipField in newShipFields)
            {
                if (shipField % rowLength != rightEdgeOfTheRow)
                {
                    fieldsToOccupy.Add(shipField+indexForNextNumberInRow);
                }

                if (shipField % rowLength != leftEdgeOfTheRow)
                {
                    fieldsToOccupy.Add(shipField-indexForNextNumberInRow);
                }
                fieldsToOccupy.Add(shipField-indexForNextNumberInColumn);
                fieldsToOccupy.Add(shipField+indexForNextNumberInColumn);
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