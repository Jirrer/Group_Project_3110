using Module8;
using System;
using System.Collections.Generic;

public class Guess
{
    private static int gridSize = 8;
    private static HashSet<Position> guessedPositions = new HashSet<Position>(); // Track all previous guesses
    private Position guess;
    private static GuessDirection lineDirection = GuessDirection.North;

    public Guess()
    {
        // Determine the next guess
        if (!lastGuessHit())
        {
            this.guess = randomGuess();
        }
        else
        {
            Guess lastGuess = Player.getLastGuess();
            Position currPosition = lastGuess.returnedGuess();
            this.guess = getNewPosition(currPosition);
        }

        // Mark the current guess as used
        guessedPositions.Add(this.guess);
    }

    private bool lastGuessHit()
    {
        if (Player.numGuesses() > 0)
        {
            return Player.getLastGuess().returnedGuess().Hit;
        }
        return false;
    }

    public Position returnedGuess()
    {
        return this.guess;
    }

    private Position getNewPosition(Position current)
    {
        int newX = current.X;
        int newY = current.Y;

        switch (lineDirection)
        {
            case GuessDirection.North:
                newY = current.Y - 1;
                break;
            case GuessDirection.East:
                newX = current.X + 1;
                break;
            case GuessDirection.South:
                newY = current.Y + 1;
                break;
            case GuessDirection.West:
                newX = current.X - 1;
                break;
            default:
                throw new InvalidOperationException("Invalid direction");
        }

        // Ensure the new position is within bounds and has not been guessed
        if (isValidPosition(newX, newY))
        {
            return new Position(newX, newY);
        }

        // If the direction is invalid, fallback to a random guess
        return randomGuess();
    }

    private Position randomGuess()
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

        return newGuess;
    }

    private bool isValidPosition(int x, int y)
    {
        // Ensure position is within grid boundaries and not already guessed
        return x >= 0 && x < gridSize && y >= 0 && y < gridSize && !guessedPositions.Contains(new Position(x, y));
    }
}
