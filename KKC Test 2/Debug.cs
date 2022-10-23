using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKC_Test_2
{
    internal class Debug
    {
        static bool Enabled = true;
        static DateTime lastTime = DateTime.Now;
        static ConsoleColor DefaultColor = ConsoleColor.Gray;
        static ConsoleColor DefaultDebugColor = ConsoleColor.Green;
        
        public Debug()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            Log(message, DefaultDebugColor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        /// <param name="displayTime"></param>
        public static void Log(string message, ConsoleColor color, bool displayTime = true)
        {
            if (Enabled)
            {
                Console.ForegroundColor = color;
                string timeDif = (DateTime.Now - lastTime).TotalMilliseconds.ToString();
                lastTime = DateTime.Now;
                string time = "";
                if(displayTime)
                    time = $"{DateTime.Now.ToString("hh:mm:ss:fff")} ({timeDif}ms): ";
                Console.WriteLine($"{time}{message}");
                Console.ForegroundColor = DefaultColor;
            }
        }
    }
}
