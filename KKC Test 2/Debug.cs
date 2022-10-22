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
        public Debug()
        {

        }
        public static void Log(string message)
        {
            if (Enabled)
            {
                var current = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                string timeDif = (DateTime.Now - lastTime).TotalMilliseconds.ToString();
                lastTime = DateTime.Now;
                Console.WriteLine($"{DateTime.Now.ToString("hh:mm:ss:fff")} ({timeDif}ms): {message}");
                Console.ForegroundColor = current;
            }
            
        }
    }
}
