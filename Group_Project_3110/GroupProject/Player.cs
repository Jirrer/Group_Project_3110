using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Module8;

public class Player : IPlayer
{
    // Tracking guesses
    static private ArrayList guesses = new ArrayList(); 
    private List<Position> guessedPositions = new List<Position>(); 
    private Position? lastHit = null; 
    private bool lastGuessHit = false; 
    private int gridSize; 

    // Player properties
    public string Name { get; private set; } = "Player"; 
    public int Index { get; private set; }

    // Constructor with default name handling
    public Player(string name = "Player") 
    {
        Name = string.IsNullOrWhiteSpace(name) ? "Player" : name; // Error handling: Assign default name if input is null or whitespace
    }

    // Method to get the next attack position
    public Position GetAttackPosition()
    {
        // If the last guess was a hit, try to target an adjacent position
        if (lastGuessHit && lastHit != null)
        {
            Position nextTarget = GetNextTarget(lastHit); 
            if (!guessedPositions.Contains(nextTarget))
            {
                guessedPositions.Add(nextTarget);
                return nextTarget;
            }
        }

        // If no valid adjacent positions, fall back to random guessing
        Position randomGuess = GenerateUniqueRandomGuess();
        guessedPositions.Add(randomGuess); 
        return randomGuess;
    }

    // method to get the number of guesses made
    static public int numGuesses()
    {
        return guesses.Count - 1; 
    }

    // Method for last guess
    static public Guess getLastGuess()
    {
        return guesses.Count > 0 ? guesses[guesses.Count - 1] as Guess : null;
    }

    // Method to handle the results of an attack
    public void SetAttackResults(List<AttackResult> results)
    {
        foreach (var result in results)
        {
            // Log which player attacked and the position
            Console.WriteLine($"Player {result.PlayerIndex} attacked position {result.Position}");
            guessedPositions.Add(result.Position); 
            // Process results only for the current player
            if (result.PlayerIndex == this.Index)
            {
                switch (result.ResultType)
                {
                    case AttackResultType.Miss:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The attack missed!");
                        Console.ResetColor();
                        lastGuessHit = false; 
                        break;

                    case AttackResultType.Hit:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The attack hit a ship!");
                        Console.ResetColor();
                        lastHit = result.Position; 
                        lastGuessHit = true; 
                        break;

                    case AttackResultType.Sank:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"The attack sank a {result.SunkShip}!");
                        Console.ResetColor();
                        lastHit = null; 
                        lastGuessHit = false; 
                        break;

                    default:
                        Console.WriteLine("Unknown attack result.");
                        break;
                }
            }
        }
        Console.WriteLine(); 
    }

    // Method to initialize the player for a new game
    public void StartNewGame(int playerIndex, int gridSize, Ships ships)
    {
        Index = playerIndex; 
        this.gridSize = gridSize; 
        Random random = new Random(); 
        List<Position> occupiedPositions = new List<Position>();

        foreach (var ship in ships._ships)
        {
            bool placed = false; 

            while (!placed)
            {
                // Generate a random position and direction
                Position position = new Position(random.Next(gridSize), random.Next(gridSize));
                Direction direction = (Direction)random.Next(2);

                // Place the ship if the position is valid
                if (CanPlaceShip(ship, position.X, position.Y, direction, gridSize, occupiedPositions))
                {
                    ship.Place(position, direction); 
                    occupiedPositions.AddRange(ship.OccupiedPositions);
                    placed = true; 
                }
            }
        }
    }

    // Method to check if a ship can be placed
    private bool CanPlaceShip(Ship ship, int startX, int startY, Direction direction, int gridSize, List<Position> occupiedPositions)
    {
        for (int i = 0; i < ship.Length; i++)
        {
            int x = startX + (direction == Direction.Horizontal ? i : 0);
            int y = startY + (direction == Direction.Vertical ? i : 0);

            // Check grid boundaries
            if (x >= gridSize || y >= gridSize)
            {
                return false; 
            }

            // Check overlap with occupied positions
            if (occupiedPositions.Any(pos => pos.X == x && pos.Y == y))
            {
                return false;
            }
        }

        return true; 
    }

    // Generate the next target position (adjacent to the last hit)
    private Position GetNextTarget(Position hitPosition)
    {
        List<Position> adjacentPositions = new List<Position>
        {
            new Position(hitPosition.X + 1, hitPosition.Y),
            new Position(hitPosition.X - 1, hitPosition.Y),
            new Position(hitPosition.X, hitPosition.Y + 1),
            new Position(hitPosition.X, hitPosition.Y - 1)
        };

        // Return the first valid, unguessed adjacent position
        return adjacentPositions.FirstOrDefault(pos => !guessedPositions.Contains(pos) && pos.X >= 0 && pos.Y >= 0)
               ?? GenerateUniqueRandomGuess();
    }

    // Generate a unique random guess
    private Position GenerateUniqueRandomGuess()
    {
        Random random = new Random();
        Position newGuess;

        do
        {
            newGuess = new Position(random.Next(gridSize), random.Next(gridSize));
        } while (guessedPositions.Contains(newGuess));

        return newGuess;
    }
}
