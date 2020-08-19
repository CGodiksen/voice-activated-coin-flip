using System;
using Microsoft.CognitiveServices.Speech;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;

namespace VoiceActivatedCoinFlip
{

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
                return "Heads";
            }
            else
            {
                tailsCounter += 1;
                return "Tails";
            }

        }

        // Returns a string containing statistics like the previous results of the coin and the users win percentage.
        private string GetStatistics()
        {
            double winPercentage = 0;
            if(winCounter + lossCounter != 0)
            {
                winPercentage = winCounter / (winCounter + lossCounter) * 100;
            }
            return $"Heads: {headCounter}\nTails: {tailsCounter}\nWin percentage: {winPercentage:0.00}\n";
        }

        // Starts an infinite loop that continuously flips a coin and decides whether or not user has won based on the user input.
        public async Task Play()
        {
            // Loading the subscription key and service region from the settings file.
            Settings settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "settings.json")));

            var config = SpeechConfig.FromSubscription(settings.SubscriptionKey, settings.ServiceRegion);

            // Using the created config to create a new speech recognizer that can be used to convert speech to text.
            using var recognizer = new SpeechRecognizer(config);

            // Adding the words that the speech recognizer should listen for to the grammer list. This ensures that the recognizer hears "Tails" and not "Tales".
            var phraseList = PhraseListGrammar.FromRecognizer(recognizer);
            phraseList.AddPhrase("Heads");
            phraseList.AddPhrase("Tails");
            phraseList.AddPhrase("Stop");
            
            string userChoice = "";
            
            while (true)
            {
                Console.WriteLine("Say heads or tails:");

                // Recognizing a single input from the microphone, meaning that the recognizer stops after the first word.
                var result = await recognizer.RecognizeOnceAsync();

                // Going through the possible reasons a recognition result might be generated.
                switch (result.Reason)
                {
                    case ResultReason.RecognizedSpeech:
                        userChoice = result.Text.Replace(".", "");
                        Console.WriteLine($"You said \"{userChoice}\"");
                        break;
                    case ResultReason.NoMatch:
                        Console.WriteLine("Speech could not be recognized.\n");
                        break;
                    default:
                        Console.WriteLine("There was a problem with the speech recognizer");
                        break;
                }

                // If the user said heads or tails then we flip the coin and report the result of the coin toss to the user, while also updating the statistics.
                if (userChoice == "Heads" || userChoice == "Tails")
                {
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
                
                // Stopping the application if the user said "Stop".
                if (userChoice == "Stop")
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
