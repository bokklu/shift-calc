using ShiftLifetimeCalculator.Helpers;
using ShiftLifetimeCalculator.IO;
using ShiftLifetimeCalculator.Models;

namespace ShiftLifetimeCalculator.Application
{
    public class CommandLineParser
    {
        public void Calculate(CmdOptions options)
        {
            if (string.IsNullOrEmpty(options.Path))
            {
                ConsoleFormatter.PrintErrorMessage($"Invalid path provided, {options.Path}");
                return;
            }

            using var workbookRepository = new WorkbookRepository(options.Path);
            var calculatorService = new CalculatorService();

            var daySheets = workbookRepository.Read();

            var (isSuccess, maybeSheet) = calculatorService.TryCalculate(daySheets);

            if (!isSuccess)
                return;

            workbookRepository.Write(maybeSheet);
        }
    }
}
