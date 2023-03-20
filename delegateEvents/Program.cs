using System;

namespace delegatesAndEvents
{
    // Create a delegate
    public delegate void RaceDelegate(int winner);

    public class Race
    {
        // Create a delegate event object
        public event RaceDelegate RaceEvent;

        public void Racing(int contestants, int laps)
        {
            Console.WriteLine("Ready\nSet\nGo!");
            Random r = new Random();
            int[] participants = new int[contestants];
            bool done = false;
            int champ = -1;

            // First to finish specified number of laps wins
            while (!done)
            {
                for (int i = 0; i < contestants; i++)
                {
                    if (participants[i] < laps)
                    {
                        participants[i] += r.Next(1, 5);
                    }
                    else
                    {
                        champ = i;
                        done = true;
                        continue;
                    }
                }
            }

            TheWinner(champ);
        }

        private void TheWinner(int champ)
        {
            Console.WriteLine("We have a winner!");

            // Invoke the delegate event object and pass champ to the method
            RaceEvent?.Invoke(champ);
        }
    }

    class Program
    {
        public static void Main()
        {
            // Create a class object
            Race round1 = new Race();

            // Register with the footRace event
            round1.RaceEvent += footRace;

            // Trigger the event
            round1.Racing(10, 50); // 10 contestants and 50 laps

            // Register with the carRace event
            round1.RaceEvent -= footRace;
            round1.RaceEvent += carRace;

            // Trigger the event
            round1.Racing(10, 100); // 10 contestants and 100 laps

            // Register a bike race event using a lambda expression
            round1.RaceEvent -= carRace;
            round1.RaceEvent += (winner) => Console.WriteLine($"Biker number {winner} is the winner.");

            // Trigger the event
            round1.Racing(10, 75); // 10 contestants and 75 laps
        }

        // Event handlers
        public static void carRace(int winner)
        {
            Console.WriteLine($"Car number {winner} is the winner.");
        }

        public static void footRace(int winner)
        {
            Console.WriteLine($"Racer number {winner} is the winner.");
        }
    }
}
