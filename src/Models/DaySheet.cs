using System.Collections.Generic;

namespace ShiftLifetimeCalculator.Models
{
    public class DaySheet
    {
        public IDictionary<Context, Cell> Values = new Dictionary<Context, Cell>
        {
            { Context.Duration, Cell.Create(3, 7) },
            { Context.GrossIncome, Cell.Create(6, 1) },
            { Context.VatDeduction, Cell.Create(6, 3) },
            { Context.FuelExpense, Cell.Create(6, 5) },
            { Context.NetIncome, Cell.Create(6, 7) },
            { Context.HourlyIncome, Cell.Create(4, 10) },
            { Context.HourlyIncomePerPerson, Cell.Create(4, 11) }
        };

        private DaySheet() {}

        public static DaySheet Create() => new DaySheet();
    }
}
