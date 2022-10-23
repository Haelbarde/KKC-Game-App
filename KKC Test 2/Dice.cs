using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKC_Test_2
{
    internal class Dice
    {
        static Random random = new Random();
        Dice()
        {
            
        }

        /// <summary>
        /// Roll a n-sided dice. 
        /// </summary>
        /// <param name="n">Number of sides on the dice.</param>
        /// <returns>Returns number from 1 to n.</returns>
        public static int Roll(int n)
        {
            return random.Next(1, n + 1);
        }

        /// <summary>
        /// Roll a n-sided dice, but includes a message for the Debug log.
        /// </summary>
        /// <param name="n">Number of sides on the dice.</param>
        /// <param name="message">Message to output into the Debug Log.</param>
        /// <returns>Returns number from 1 to n.</returns>
        public static int Roll(int n, string message)
        {
            int i = Roll(n);
            Debug.Log($"Rolling a {n}-sided dice for {message}. Rolled a {i}.", ConsoleColor.Cyan);
            return Roll(n);
        }
    }
}
