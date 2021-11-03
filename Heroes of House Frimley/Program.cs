using Heroes_of_House_Frimley.Classes;
using System;

namespace Heroes_of_House_Frimley
{
    class Program
    {
        static void Main(string[] args)
        {
            // New game is created.
            var game = new Game();
            game.Quest();

            Console.WriteLine("Thanks for playing!");
        }
    }
}
