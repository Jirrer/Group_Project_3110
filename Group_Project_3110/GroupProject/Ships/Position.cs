namespace Module8
{
    public class Position
    {
        // Properties for position coordinates and hit status
        public int X { get; set; }
        public int Y { get; set; }
        public bool Hit { get; set; }

        // Default constructor
        public Position()
        {
            X = 0;
            Y = 0;
            Hit = false; // Default to not hit
        }

        // Constructor to initialize position and default hit status
        public Position(int x, int y)
        {
            X = x;
            Y = y;
            Hit = false; // Default to not hit
        }

        // Override ToString() for better debugging/logging
        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        // Override Equals to compare positions by X and Y values
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Position)obj;
            return X == other.X && Y == other.Y;
        }

        // Override GetHashCode for collections like HashSet or Dictionary
        public override int GetHashCode()
        {
            return X * 31 + Y; // Prime for better hash distribution
        }
    }
}
