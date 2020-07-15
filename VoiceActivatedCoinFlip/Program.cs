using System;
using System.Threading.Tasks;

namespace VoiceActivatedCoinFlip
{
    class Program
    {
        static async Task Main()
        {
            CoinFlip CoinFlip = new CoinFlip();
            await CoinFlip.Play();

            Console.ReadLine();
        }
    }
}
