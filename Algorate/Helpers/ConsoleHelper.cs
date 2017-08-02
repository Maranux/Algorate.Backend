using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algorate.Helpers
{
    public static class ConsoleHelper
    {
        public static void Success(string message)
        {
            if (message == null) return;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Error(string message)
        {
            if (message == null) return;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Warning(string message)
        {
            if (message == null) return;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Thread(string message)
        {
            if (message == null) return;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Info(string message)
        {
            if (message == null) return;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
