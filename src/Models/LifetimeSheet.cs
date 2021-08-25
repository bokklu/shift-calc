using System.Collections.Generic;

namespace ShiftLifetimeCalculator.Models
{
    public class LifetimeSheet
    {
        public IDictionary<Context, Cell> Values = new Dictionary<Context, Cell>
        {
            { Context.LifetimeHours, Cell.Create(2, 4) },
            { Context.LifetimeGrossIncome, Cell.Create(5, 1) },
            { Context.LifetimeVatDeduction, Cell.Create(5, 2) },
            { Context.LifetimeFuelExpense, Cell.Create(5, 3) },
            { Context.LifetimeNetIncome, Cell.Create(5, 4) },
            { Context.AverageHourlyIncome, Cell.Create(3, 7) },
            { Context.AverageHourlyIncomePerPerson, Cell.Create(3, 8) },
        };

        private LifetimeSheet() {}

        public static LifetimeSheet Create() => new LifetimeSheet();
    }
}
