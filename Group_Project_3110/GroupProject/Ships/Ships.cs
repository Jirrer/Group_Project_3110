using System;
using System.Collections.Generic;

namespace Module8
{
    public class Ships
    {
        public readonly List<Ship> _ships = new List<Ship>(); // List of ships

        // Method to clear all ships
        public void Clear()
        {
            _ships.Clear();
        }

        // Property to check if the player's battleship is sunk
        public bool SunkMyBattleShip
        {
            get
            {
                foreach (var ship in _ships)
                {
                    if (ship.IsBattleShip && ship.Sunk)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        // Method to add a ship to the collection
        public void Add(Ship ship)
        {
            _ships.Add(ship);
        }

        // Method to handle an attack and return the result
        public AttackResult Attack(Position pos)
        {
            foreach (var ship in _ships)
            {
                var attackResult = ship.Attack(pos);
                if (attackResult.ResultType != AttackResultType.Miss)
                {
                    return attackResult; // Return if hit or sank
                }
            }
            return new AttackResult(0, pos); // Miss
        }
    }
}
