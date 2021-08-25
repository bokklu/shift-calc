using CommandLine;
using ShiftLifetimeCalculator.Application;
using ShiftLifetimeCalculator.Helpers;
using ShiftLifetimeCalculator.Models;
using System;

namespace ShiftLifetimeCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<CmdOptions>(args).WithParsed(opts => new CommandLineParser().Calculate(opts));
                ConsoleFormatter.PrintSuccessMessage("Lifetime stats calculated!");
            }
            catch (Exception e)
            {
                ConsoleFormatter.PrintErrorMessage($"Something went wrong, Exception={e}");
                throw;
            }
        }
    }
}
