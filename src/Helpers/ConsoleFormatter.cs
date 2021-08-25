using System;

namespace ShiftLifetimeCalculator.Helpers
{
    public static class ConsoleFormatter
    {
        public static void PrintSuccessMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            PrintMessage(text);
        }

        public static void PrintErrorMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintMessage(text);
        }

        private static void PrintMessage(string text)
        {
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
