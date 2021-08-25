using CommandLine;

namespace ShiftLifetimeCalculator.Models
{
    [Verb("calculate", HelpText = "Calculates the lifetime sheet stats")]
    public class CmdOptions
    {
        [Option('p', "path", Required = true, HelpText = "Excel Workbook Path")]
        public string Path { get; set; }
    }
}
