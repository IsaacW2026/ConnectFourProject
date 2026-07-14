using System;
using System.Threading;
// Connect Four Game - Isaac Withers
namespace ConnectFourProject
{
    internal class Program
    {
        static void OutputBoard(int[,] board, int player) //Subrountine to output the Connect Four board.
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Current Player: " + player); //Outputs which player's turn it is.
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1 2 3 4 5 6 7");
            Console.WriteLine("-------------");


            for (int row = 0; row < board.GetLength(0); row++) //Iterates over each row, and outputs it.
            {
                for (int col = 0; col < board.GetLength(1); col++) //Also iterates over each column
                {
                    switch (board[row, col]) //Switch statement to apply the colours to the board
                    {
                        case 1: //If it is player 1, set the colour to cyan, and output 1
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(board[row, col] + " ");
                            break;
                        case 2: //If it is player 2, set the colour to yellow, and output 2
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(board[row, col] + " ");
                            break;
                        default: //If it isn't player 1 or 2 (empty), set the colour to yellow and output 0
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(board[row, col] + " ");
                            break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.White; //Resets back to white
                Console.WriteLine(""); //Moves onto a new line for each row

            }
        }

        static bool CheckWin(int[,] board, int player) //Function to check if the player has won. Player passed in as a function. Returns true if the player has won.
        {
            int rows = 6; //There are 6 rows
            int cols = 7; //There are 7 columns


            for (int row = 0; row < rows; row++) //Horizontal Direction
            {
                for (int col = 0; col < cols - 3; col++) //Iterate over every colum
                {
                    if (board[row, col] == player && board[row, col + 1] == player && board[row, col + 2] == player && board[row, col + 3] == player) //Checks each column by seeing if all 4 are equal.
                    {
                        return true;
                    }
                }
            }
            for (int row = 0; row < rows - 3; row++) //Vertical direction. Iterates over each row. Uses -3 to avoid IndexError
            {
                for (int col = 0; col < cols; col++)  // Iterates over every column, and checks if all 4 are equal.
                {
                    if (board[row, col] == player && board[row + 1, col] == player && board[row + 2, col] == player && board[row + 3, col] == player)
                    {
                        return true;
                    }
                }
            }
            for (int row = 3; row < rows; row++) //Diagonal (/). Uses row = 3 to avoid IndexError
            {
                for (int col = 0; col < cols - 3; col++) //Uses cols - 3 to avoid being out of range in board list.
                {
                    if (board[row, col] == player && board[row - 1, col + 1] == player && board[row - 2, col + 2] == player && board[row - 3, col + 3] == player)
                    {
                        return true;
                    }
                }
            }
            for (int row = 0; row < rows - 3; row++) // Diagonal (\). 
            {
                for (int col = 0; col < cols - 3; col++)
                {
                    if (board[row, col] == player && board[row + 1, col + 1] == player && board[row + 2, col + 2] == player && board[row + 3, col + 3] == player)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static int PlacePiece(int[,] board, int column, int player)
        {
            bool placed = false; // Variable to hold whether player's piece has been placed.

            for (int row = board.GetLength(0) - 1; row >= 0; row--) //Places the piece as far down as possible in the column. Loops through all rows of the board. 
            {

                if (board[row, column - 1] == 0) //If it is equal to zero, nothing is placed there.
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray; //Highlight in grey
                    Console.WriteLine("Player {0} chose column: {1}", player, column); //If it is player two, output what the computer chose
                    Console.BackgroundColor = ConsoleColor.Black;
                    Thread.Sleep(550); //Waits for 550ms to give user time to see message before clearing

                    board[row, column - 1] = player; //So player's piece can be played.
                    if (player == 1) //Changes player over
                    {
                        player = 2;
                    }

                    else
                    {
                        player = 1;
                    }
                    placed = true; //Placed is set to true, so that the column full message doesn't show.
                    Console.Clear(); //Clears the console so it is easier to read.
                    break;
                }

            }

            if (!placed) //If it's not placed, the column must be full. User gets to re-place
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("COLUMN IS FULL: Try a different one.");
                Thread.Sleep(200);
                Console.ForegroundColor = ConsoleColor.White;

            }
            return player;

        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow; //Sets Console colour for welcome message
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to Connect Four!");
            Console.ForegroundColor = ConsoleColor.White; //Resets back to default colour
            Console.BackgroundColor = ConsoleColor.Black;

            int[,] board =
                {
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 }
                }; //Connect Four board.

            int player = 1;
            bool gameWon = false;
            Console.WriteLine("Please select a game mode. Enter Computer to play with the computer, or press Enter to play with a friend");
            string gameMode = Console.ReadLine();


            while (!gameWon) //Loop the game while a player has not won.
            {
                OutputBoard(board, player); //Outputs current connect four board.
                int column = 0; //Declare variable to hold user's selected column
                int turnCount = 0; // Variable to count the number of turns taken


                if (player == 2 && gameMode.ToLower() == "computer")
                {
                    bool moveChosen = false;


                    if (turnCount > 2) //Only try to win if the user has had a few turns, to try and give the human a chance!
                    {
                        // 1. Try to win
                        for (int testCol = 1; testCol <= 7; testCol++) //Iterates over each column to test if the computer can win in that column.
                        {
                            int[,] tempBoard = (int[,])board.Clone();
                            for (int row = tempBoard.GetLength(0) - 1; row >= 0; row--) //Iterates from the bottom row to the top
                            {
                                if (tempBoard[row, testCol - 1] == 0)
                                {
                                    tempBoard[row, testCol - 1] = 2; // Simulate computer move
                                    if (CheckWin(tempBoard, 2)) //Test if this move would win the game
                                    {
                                        column = testCol;
                                        moveChosen = true;
                                    }
                                    break;
                                }
                            }
                            if (moveChosen) break;
                        }

                        // 2. Try to block Player 1
                        if (!moveChosen) ///If no winning move found, try to block Player 1
                        {
                            for (int testCol = 1; testCol <= 7; testCol++)
                            {
                                int[,] tempBoard = (int[,])board.Clone();
                                for (int row = tempBoard.GetLength(0) - 1; row >= 0; row--) //Iterates from the bottom row to the top
                                {
                                    if (tempBoard[row, testCol - 1] == 0)
                                    {
                                        tempBoard[row, testCol - 1] = 1; // Simulate Player 1 move
                                        if (CheckWin(tempBoard, 1))
                                        {
                                            column = testCol;
                                            moveChosen = true;
                                        }
                                        break;
                                    }
                                }
                                if (moveChosen) break;
                            }
                        }
                    }
                    // 3. Random move if no win or block
                    if (!moveChosen)
                    {
                        Random numberGenerator = new Random();
                        column = numberGenerator.Next(1, 8);
                    }
                }

                else
                {
                    Console.WriteLine("Player {0}, please enter a column to place your piece (1-7):", player);
                    string input = Console.ReadLine(); //Prompts player to enter their selected column.
                    
                    
                    if (!int.TryParse(input, out column) || column < 1 || column > 7) //If the user inputs anything that is not a number, or is greater than 7 or less than 1, output error message.
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red; //Highlights in red
                        Console.WriteLine("Invalid input. Try again.");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }

                }
                player = PlacePiece(board, column, player); //Calls function to place the pieace and returns which player is next.
                turnCount++;


                if (CheckWin(board, 1)) //Check if player 1 has won
                {
                    Console.Clear();
                    OutputBoard(board, player);
                    Console.BackgroundColor = ConsoleColor.White; //Highlights the text
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Player 1 wins!");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    gameWon = true; //Stops the loop
                    Console.ReadLine();
                }

                else if (CheckWin(board, 2)) //Checks if player 2 has won
                {
                    Console.Clear();
                    OutputBoard(board, player);
                    Console.BackgroundColor = ConsoleColor.White; //Highlights the text
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Player 2 wins!");
                    gameWon = true; //Stops the loop
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadLine(); //stops it from closing immediately.
                }

            }
        }
    }
}
