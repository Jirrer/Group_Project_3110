using System;
using System.Linq;

namespace Module8
{
    public abstract class Ship
    {
        private readonly Position[] _positions; // Positions occupied by this ship
        public readonly int Length; // Ship length
        public readonly ConsoleColor Color; // Color for display
        public readonly ShipTypes ShipType; // Type of the ship
        private static readonly char[] Characters = { '?', 'P', 'S', 'D', 'A', 'B' }; // Display characters for each type

        // Property to check if this ship is a battleship
        public virtual bool IsBattleShip => false;

        // Constructor to initialize ship properties
        protected Ship(int length, ConsoleColor color, ShipTypes shipType)
        {
            Length = length;
            Color = color;
            ShipType = shipType;
            _positions = new Position[length];
        }

        // Property to return the positions occupied by the ship
        public Position[] Positions => _positions;

        // Property to return occupied positions (alias for Positions for clarity)
        public IEnumerable<Position> OccupiedPositions => _positions;

        // Method to place the ship on the grid
        public void Place(Position start, Direction direction)
        {
            for (int i = 0; i < Length; i++)
            {
                _positions[i] = new Position(start.X, start.Y);
                if (direction == Direction.Horizontal) start.X++;
                if (direction == Direction.Vertical) start.Y++;
            }
        }

        // Method to handle an attack on the ship
        public AttackResult Attack(Position pos)
        {
            foreach (var position in _positions)
            {
                if (position.X == pos.X && position.Y == pos.Y)
                {
                    position.Hit = true;
                    if (Sunk)
                    {
                        return new AttackResult(0, pos, AttackResultType.Sank, ShipType);
                    }
                    return new AttackResult(0, pos, AttackResultType.Hit);
                }
            }
            return new AttackResult(0, pos); // Miss
        }

        // Property to check if the ship is sunk
        public bool Sunk => _positions.All(position => position.Hit);

        // Property to get the character for display
        public char Character => Characters[(int)ShipType];
    }
}

