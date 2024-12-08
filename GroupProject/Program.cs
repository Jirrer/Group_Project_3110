// Multiplayer Battleship Game with AI - Partial Solution

using System.Collections.Generic;
using System;

namespace Module8
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IPlayer> players = new List<IPlayer>();
            players.Add(new DumbPlayer("Dumb 1"));
            // players.Add(new DumbPlayer("Dumb 2"));
            // players.Add(new DumbPlayer("Dumb 3"));
            players.Add(new RandomPlayer("Random 1"));
            // players.Add(new RandomPlayer("Random 2"));
            // players.Add(new RandomPlayer("Random 3"));
            // players.Add(new RandomPlayer("Random 4"));
            // players.Add(new RandomPlayer("Random 5"));


            players.Add(new Player("Our Player"));

            // Your code here
            players.Add(new GroupPlayer("Group Player"));

            MultiPlayerBattleShip game = new MultiPlayerBattleShip(players);
            game.Play(PlayMode.Pause);  // Play the game with this "play mode"
        }
    }
    
    public class GroupNPlayer : IPlayer
    {
        private string _name;
        private int _index;
        private int _gridSize;

        public GroupNPlayer(string name)
        {
            _name = name;
        }

        public string Name => _name;

        public int Index => _index;

        public void StartNewGame(int playerIndex, int gridSize, Ships ships)
        {
            _index = playerIndex;
            _gridSize = gridSize;

            // Example ship placement strategy: Diagonal placement
            int x = 0, y = 0;
            foreach (var ship in ships._ships)
            {
                ship.Place(new Position(x, y), Direction.Horizontal);
                x++;
                y++;
            }
        }

        public Position GetAttackPosition()
        {
            // Example attack strategy: Random guessing
            Random random = new Random();
            int x = random.Next(0, _gridSize);
            int y = random.Next(0, _gridSize);

            return new Position(x, y);
        }

        public void SetAttackResults(List<AttackResult> results)
        {
            // Example: Log results to the console
            foreach (var result in results)
            {
                Console.WriteLine($"Player {result.PlayerIndex} attacked {result.Position}. Result: {result.ResultType}");
            }
        }
    }
}
