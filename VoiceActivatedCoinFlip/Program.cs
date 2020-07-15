using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using System.IO;

namespace VoiceActivatedCoinFlip
{
    class Program
    {
        static async Task Main()
        {
            await RecognizeSpeechAsync();

            Console.WriteLine("Please press any key to continue...");
            Console.ReadLine();
        }

        static async Task RecognizeSpeechAsync()
        {
            // Loading the subscription key and service region from the settings file.
            Settings settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(@"C:\Users\chris\source\repos\VoiceActivatedCoinFlip\VoiceActivatedCoinFlip\settings.json"));

            var config = SpeechConfig.FromSubscription(settings.SubscriptionKey, settings.ServiceRegion);

            using var recognizer = new SpeechRecognizer(config);

            var result = await recognizer.RecognizeOnceAsync();
            switch (result.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    Console.WriteLine($"We recognized: {result.Text}");
                    break;
                case ResultReason.NoMatch:
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                    break;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                    break;
            }
        }
    }
}
