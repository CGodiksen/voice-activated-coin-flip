using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceActivatedCoinFlip
{

    // Make it so a secret word always makes the player lose.
    class CoinFlip
    {
        private int headCounter = 0;
        private int tailsCounter = 0;
        private double winCounter = 0;
        private double lossCounter = 0;
        
        // Randomly decides whether the coin landed heads or tails.
        private string FlipCoin()
        {
            Random random = new Random();
            if(random.Next(1, 3) == 1)
            {
                headCounter += 1;
                return "heads";
            }
            else
            {
                tailsCounter += 1;
                return "tails";
            }

        }

        // Returns a string containing statistics like the previues results of the coin and the users win percentage.
        private string GetStatistics()
        {
            double winPercentage = 0;
            if(winCounter + lossCounter != 0)
            {
                winPercentage = winCounter / (winCounter + lossCounter) * 100;
            }
            return $"Head: {headCounter}\nTails: {tailsCounter}\nWin percentage: {winPercentage:0.00}\n";
        }

        // Starts an infinite loop that continuously flips a coin and decides whether or not user has won based on the user input.
        public void Play()
        {
            while (true)
            {
                string userChoice = "";
                Console.WriteLine("Enter heads or tails:");

                while (userChoice != "tails" && userChoice != "heads")
                {
                    userChoice = Console.ReadLine().ToLower();

                    if(userChoice != "tails" && userChoice != "heads")
                    {
                        Console.WriteLine("Enter heads or tails:");
                    }
                }

                string flippedCoin = FlipCoin();

                if (flippedCoin == userChoice)
                {
                    winCounter += 1;
                    Console.WriteLine($"The coin landed {flippedCoin}, You won!\n");
                }
                else
                {
                    lossCounter += 1;
                    Console.WriteLine($"The coin landed {flippedCoin}, You lost!\n");
                }

                Console.WriteLine(GetStatistics());
            }
        }
    }
}
