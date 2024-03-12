using System;

namespace MastermindGame
{
    
    class Program
    {
        static void Main()
        {
            // Generate a random 4-digit answer
            Random random = new Random();
            int[] answer = new int[4];
            for (int i = 0; i < 4; i++)
            {
                answer[i] = random.Next(1, 7);
            }

            // Game loop
            for (int attempts = 1; attempts <= 10; attempts++)
            {
                // Get player input
                int[] guess = GetPlayerGuess();

                // Check the guess
                int[] feedback = CheckGuess(answer, guess);

                // Display feedback
                Console.WriteLine("Feedback: " + string.Join("", feedback));

                // Check if the player has guessed the correct answer
                if (IsCorrectGuess(feedback))
                {
                    Console.WriteLine("Congratulations! You guessed the correct answer.");
                    break;
                }

                // Display remaining attempts
                Console.WriteLine($"Attempts remaining: {10 - attempts}\n");

                // If no correct guess within 10 attempts, display the correct answer
                if (attempts == 10)
                {
                    Console.WriteLine($"Sorry! You have run out of attempts. The correct answer was: {string.Join("", answer)}");
                }
            }
        }

        static int[] GetPlayerGuess()
        {
            Console.Write("Enter your guess (4 digits between 1 and 6): ");
            string input = Console.ReadLine();

            // Validate input
            while (input.Length != 4 || !IsNumeric(input) || !IsValidDigits(input))
            {
                Console.Write("Invalid input. Please enter a valid guess: ");
                input = Console.ReadLine();
            }

            // Convert input to integer array
            int[] guess = new int[4];
            for (int i = 0; i < 4; i++)
            {
                guess[i] = int.Parse(input[i].ToString());
            }

            return guess;
        }

        static bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }

        static bool IsValidDigits(string input)
        {
            foreach (char digit in input)
            {
                int num = int.Parse(digit.ToString());
                if (num < 1 || num > 6)
                {
                    return false;
                }
            }
            return true;
        }

        static int[] CheckGuess(int[] answer, int[] guess)
        {
            int[] feedback = new int[4];

            // Check for correct digits in correct positions
            for (int i = 0; i < 4; i++)
            {
                if (guess[i] == answer[i])
                {
                    feedback[i] = 1; // 1 represents correct digit in correct position
                }
            }

            // Check for correct digits in wrong positions
            for (int i = 0; i < 4; i++)
            {
                if (feedback[i] == 0)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (i != j && guess[i] == answer[j] && feedback[j] == 0)
                        {
                            feedback[j] = -1; // -1 represents correct digit in wrong position
                            break;
                        }
                    }
                }
            }

            return feedback;
        }

        static bool IsCorrectGuess(int[] feedback)
        {
            return Array.TrueForAll(feedback, f => f == 1);
        }
    }

}
