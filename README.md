# Voice activated coin flip
Simple console program that uses speech recognition to flip a coin and determine a winner. The program uses Microsoft Azure speech recognition which means an Azure
subscription is necessary to run the program.

To input the information from your Azure subscription, create a "settings.json" file in the main directory with the following format:
```
{
  "SubscriptionKey": "YourSubscriptionKey",
  "ServiceRegion": "YourServiceRegion"
}
```

Simply say "Heads" or "Tails" to pick the side of the coin that you want and the program will flip the coin. Saying "Stop" stops the program.
