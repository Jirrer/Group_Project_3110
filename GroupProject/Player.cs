using System.Collections;
using System;
using Module8;
using System.Transactions;

public class Player : IPlayer
{
    static private ArrayList guesses = new ArrayList(); 

    public string Name { get; private set; }
    public int Index { get; private set; }

    public Player(string name) {
        this.Name = name;
    }

    public Position GetAttackPosition() {
        Guess currGuess = new Guess();
        
        guesses.Add(currGuess);

        return currGuess.returnedGuess();
    }

    static public int numGuesses() {
        return guesses.Count - 1;
    }

    static public Guess getLastGuess() {
        return (Guess)guesses[guesses.Count - 1];
    }


    public void SetAttackResults(List<AttackResult> results) //needs this done for rest of methods to know if the grid has been hit or not
    {
        foreach (var result in results)
        {
            Console.WriteLine($"Player {result.PlayerIndex}  attacked position {result.Position}");
        
            switch (result.ResultType)
            {
                case AttackResultType.Miss:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The attack missed!");
                    Console.ResetColor();
                    break;
        
                case AttackResultType.Hit:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The attack hit a ship!");
                    Console.ResetColor();
                    break;
        
                case AttackResultType.Sank:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"The attack sank a {result.SunkShip}!");
                    Console.ResetColor();
                    break;
        
                default:
                    Console.WriteLine("Unknown attack result.");
                    break;
            }
        Console.WriteLine();
        }
    }

    public void StartNewGame(int playerIndex, int gridSize, Ships ships) // Not done yet
    {
        int y = 0;
            foreach (var ship in ships._ships)
            {
                ship.Place(new Position(0, y++), Direction.Horizontal);
            }
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
}
