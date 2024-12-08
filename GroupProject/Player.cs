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
            Console.WriteLine($"Player {result.PlayerIndex} attacked position {result.Position}");
        
            switch (result.ResultType)
            {
                case AttackResultType.Miss:
                    Console.WriteLine("The attack missed!");
                    break;
        
                case AttackResultType.Hit:
                    Console.WriteLine("The attack hit a ship!");
                    break;
        
                case AttackResultType.Sunk:
                    Console.WriteLine($"The attack sank a {result.SunkShip}!");
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
}
