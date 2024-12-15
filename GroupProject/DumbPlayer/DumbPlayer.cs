using System.Collections.Generic;

namespace Module8
{
    internal class DumbPlayer : IPlayer
    {
        private static int _nextGuess = 0; // Note static - we all share the same guess 

        private int _index;
        private int _gridSize;

        public DumbPlayer(string name)
        {
            Name = name;
        }

        public void StartNewGame(int playerIndex, int gridSize, Ships ships)
        {
            _gridSize = gridSize;
            _index = playerIndex;

            // DumbPlayer just puts the ships in the grid one on each row
            int y = 0;
            foreach (var ship in ships._ships)
            {
                ship.Place(new Position(0, y++), Direction.Horizontal);
            }
        }

        public Position GetAttackPosition()
        {
            // A *very* naive guessing algorithm that simply starts at 0, 0 and guess each square in order
            // All 'DumbPlayers' share the counter so they won't guess the same one
            // But we don't check to make sure the square has not been guessed before
            var pos = new Position(_nextGuess % _gridSize, (_nextGuess /_gridSize));
            _nextGuess++;
            return pos;
        }

        public void SetAttackResults(List<AttackResult> results)
        {
            // DumbPlayer does nothing with these results - its going to keep making dumb guesses
        }

        public string winPercentage() {
        Grid grid = MultiPlayerBattleShip.getGrid(Index);
        if (grid == null)
            return "Grid not found.";

        int totalCells = 0;
        int hitCells = 0;

        for (int x = 0; x < grid.GridSize; x++) 
        {
            for (int y = 0; y < grid.GridSize; y++)
            {
                totalCells++;
                if (grid.GetEntry(x, y).Hit) 
                {
                    hitCells++;
                }
            }
        }

        double percentage = totalCells > 0 ? (hitCells / (double)totalCells) * 100 : 0;

        return $"Win Percentage: {percentage:0.00}% ({hitCells}/{totalCells} cells hit)";
    }

        public string Name { get; }
        public int Index => _index;
    }
}
