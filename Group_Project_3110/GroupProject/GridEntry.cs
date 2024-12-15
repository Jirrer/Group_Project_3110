namespace Module8
{
    public class GridEntry
    {
        public bool Hit { get; set; }
        public Ship? Ship { get; set; }
    // Constructor for error handling
    public GridEntry()
        {
            Hit = false;  // Default to not hit
            Ship = null;  // Default to no ship
        }

        // Method to place a ship in this grid entry
        public void PlaceShip(Ship ship)
        {
            if (Ship != null)
            {
                throw new InvalidOperationException("A ship is already placed in this grid entry.");
            }
            Ship = ship;
        }

        // Method to mark the grid entry as hit
        public void MarkHit()
        {
            Hit = true;
        }
    }
}
