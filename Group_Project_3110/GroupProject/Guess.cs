using Module8;
using System;
using System.Collections.Generic;

public class Guess
{
    private static int gridSize = 8;
    private static HashSet<Position> guessedPositions = new HashSet<Position>(); // Track all previous guesses
    private Position guess;
    private static GuessDirection lineDirection = GuessDirection.North; // Initial direction

    public Guess()
    {
        if (!LastGuessHit())
        {
            this.guess = GenerateUniqueRandomGuess(); // If last guess missed, make a random guess
        }
        else
        {
            // Use current position to calculate the next target
            Guess lastGuess = Player.getLastGuess();
            Position currPosition = lastGuess.returnedGuess();
            this.guess = GenerateNextTargetPosition(currPosition);
        }
    }

    /// <summary>
    /// Determines if the last guess was a hit.
    /// </summary>
    private bool LastGuessHit()
    {
        if (Player.numGuesses() >= 0)
        {
            return Player.getLastGuess().returnedGuess().Hit;
        }
        else
        {
            return false; // No previous guess to evaluate
        }
    }

    /// <summary>
    /// Returns the current guess.
    /// </summary>
    public Position returnedGuess()
    {
        return this.guess;
    }

    /// <summary>
    /// Generates the next target position based on the current position.
    /// Attempts to hit adjacent cells in a specific direction.
    /// </summary>
    private Position GenerateNextTargetPosition(Position currPosition)
    {
        // Try all four directions starting from the current line direction
        for (int i = 0; i < 4; i++)
        {
            int newX = currPosition.X;
            int newY = currPosition.Y;

            // Adjust coordinates based on the current direction
            switch (lineDirection)
            {
                case GuessDirection.North: newY -= 1; break;
                case GuessDirection.East: newX += 1; break;
                case GuessDirection.South: newY += 1; break;
                case GuessDirection.West: newX -= 1; break;
                default: break;
            }

            Position target = new Position(newX, newY);

            // Validate position: within bounds, not already guessed
            if (IsValidPosition(target) && !guessedPositions.Contains(target))
            {
                guessedPositions.Add(target); // Mark as guessed
                return target;
            }

            // Rotate to the next direction if the current one fails
            lineDirection = (GuessDirection)(((int)lineDirection + 1) % 4);
        }

        // Fallback to random guessing if no valid adjacent position is found
        return GenerateUniqueRandomGuess();
    }

    /// <summary>
    /// Generates a unique random guess.
    /// Ensures the guess is within bounds and hasn't been made before.
    /// </summary>
    private Position GenerateUniqueRandomGuess()
    {
        Random random = new Random();
        Position newGuess;

        // Keep generating random guesses until a valid position is found
        do
        {
            int randomX = random.Next(0, gridSize);
            int randomY = random.Next(0, gridSize);
            newGuess = new Position(randomX, randomY);
        } while (guessedPositions.Contains(newGuess));

        // Add the new guess to the global guessed positions
        guessedPositions.Add(newGuess);

        return newGuess;
    }

    /// <summary>
    /// Validates whether a position is within the grid bounds.
    /// </summary>
    private bool IsValidPosition(Position pos)
    {
        return pos.X >= 0 && pos.X < gridSize && pos.Y >= 0 && pos.Y < gridSize;
    }
}