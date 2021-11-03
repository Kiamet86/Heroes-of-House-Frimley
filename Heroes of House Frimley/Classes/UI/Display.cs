using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_of_House_Frimley.Classes.UI
{
    public static class Display
    {
        /// <summary>
        /// Changes Console Colour to Red, outputs a string, then reverts to Gray.
        /// </summary>
        /// <param name="message"></param>
        public static void DangerRedText(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            System.Threading.Thread.Sleep(800);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void PlayerChoiceYellow(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + message);
            Console.Write("=>");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void PlayerActionGreen(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message + "\n");
            System.Threading.Thread.Sleep(800);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void EnemyActionDarkYellow(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(message);
            System.Threading.Thread.Sleep(800);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void PlayerSuccessWhite(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            System.Threading.Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void MagicBlue(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void HealthRed(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

    }
}
