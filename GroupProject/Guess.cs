using Module8;

public class Guess {
    static int gridSize = 8;
    private Position guess;
    static GuessDirection lineDirection;

    public Guess() { // needs .Hit bool to be updated with set attack for this to work
        
        if (!lastGuessHit()) {
            this.guess = randomGuess();
        } else {
            Guess lastGuess = Player.getLastGuess();
            Position currPosition = lastGuess.returnedGuess();
            
            this.guess = getNewPosition(currPosition);
        }
    }

    private bool lastGuessHit() {
        if (Player.numGuesses() >= 0) {
            return Player.getLastGuess().returnedGuess().Hit;
        } else {return false; }
    }



    public Position returnedGuess(){
        return this.guess;
    } 

    private Position getNewPosition(Position guess){ // add fix for out of bounds
        int newX = guess.X;
        int newY = guess.Y;
        
        switch (lineDirection) {
            // case GuessDirection.North: newY = guess.Y + 1; break;
            // // case GuessDirection.East: newX = guess.X - 1; break;
            // // case GuessDirection.South: newY = guess.Y - 1; break;
            // case GuessDirection.West: newY = guess.X + 1; break;

            default: break; // fix
        }

        return new Position(0, 0);
    }

    private Position randomGuess(){ // fix
        Random random = new Random();
        // add grid size, add logic for "hot zones", make sure it does not guess already guessed grids
        int randomCord1 = random.Next(0,gridSize);
        int randomCord2 = random.Next(0,gridSize);

        return new Position(randomCord1, randomCord2); // fix
    }

}