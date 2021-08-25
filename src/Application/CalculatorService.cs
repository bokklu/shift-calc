using ShiftLifetimeCalculator.Application.Validators;
using ShiftLifetimeCalculator.Helpers;
using ShiftLifetimeCalculator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ShiftLifetimeCalculator.Application
{
    public class CalculatorService
    {
        private static readonly DaySheetValidator DaySheetValidator = new DaySheetValidator();

        public static IFormatProvider CurrencyFormat
        {
            get
            {
                var currencyFormat = new NumberFormatInfo
                {
                    NegativeSign = "-",
                    CurrencyDecimalSeparator = ".",
                    CurrencyGroupSeparator = ",",
                    CurrencySymbol = "€"
                };

                return currencyFormat;
            }
        }

        public (bool isSuccess, LifetimeSheet sheet) TryCalculate(IEnumerable<DaySheet> daySheetValues)
        {
            var lifetimeSheet = LifetimeSheet.Create();

            var lifetimeHours = new TimeSpan();
            var lifetimeGrossIncome = 0M;
            var lifetimeVatDeduction = 0M;
            var lifetimeFuelExpense = 0M;
            var lifetimeNetIncome = 0M;
            var averageHourlyIncome = 0M;
            var averageHourlyIncomePerPerson = 0M;


            foreach (var daySheetValue in daySheetValues)
            {
                var validationResult = DaySheetValidator.Validate(daySheetValue);

                if (!validationResult.IsValid)
                {
                    validationResult.Errors.ForEach(x => ConsoleFormatter.PrintErrorMessage(x.ErrorMessage));
                    return (false, null);
                }

                lifetimeHours += TimeSpan.Parse(daySheetValue.Values[Context.Duration].RawValue);
                lifetimeGrossIncome += decimal.Parse(daySheetValue.Values[Context.GrossIncome].RawValue, NumberStyles.Currency, CurrencyFormat);
                lifetimeVatDeduction += decimal.Parse(daySheetValue.Values[Context.VatDeduction].RawValue, NumberStyles.Currency, CurrencyFormat);
                lifetimeFuelExpense += decimal.Parse(daySheetValue.Values[Context.FuelExpense].RawValue, NumberStyles.Currency, CurrencyFormat);
                lifetimeNetIncome += decimal.Parse(daySheetValue.Values[Context.NetIncome].RawValue, NumberStyles.Currency, CurrencyFormat);
                averageHourlyIncome += decimal.Parse(daySheetValue.Values[Context.HourlyIncome].RawValue, NumberStyles.Currency, CurrencyFormat);
                averageHourlyIncomePerPerson += decimal.Parse(daySheetValue.Values[Context.HourlyIncomePerPerson].RawValue, NumberStyles.Currency, CurrencyFormat);
            }

            var daySheetValuesCount = (decimal)daySheetValues.Count();
            averageHourlyIncome /= daySheetValuesCount;
            averageHourlyIncomePerPerson /= daySheetValuesCount;

            lifetimeSheet.Values[Context.LifetimeHours].SetRawValue(lifetimeHours.ToString("c", CultureInfo.InvariantCulture));
            lifetimeSheet.Values[Context.LifetimeGrossIncome].SetRawValue(lifetimeGrossIncome.ToString("F2", CurrencyFormat));
            lifetimeSheet.Values[Context.LifetimeVatDeduction].SetRawValue(lifetimeVatDeduction.ToString("F2", CurrencyFormat));
            lifetimeSheet.Values[Context.LifetimeFuelExpense].SetRawValue(lifetimeFuelExpense.ToString("F2", CurrencyFormat));
            lifetimeSheet.Values[Context.LifetimeNetIncome].SetRawValue(lifetimeNetIncome.ToString("F2", CurrencyFormat));
            lifetimeSheet.Values[Context.AverageHourlyIncome].SetRawValue(averageHourlyIncome.ToString("F2", CurrencyFormat));
            lifetimeSheet.Values[Context.AverageHourlyIncomePerPerson].SetRawValue(averageHourlyIncomePerPerson.ToString("F2", CurrencyFormat));

            return (true, lifetimeSheet);
        }
    }
}
