using FluentValidation;
using ShiftLifetimeCalculator.Models;
using System;
using System.Globalization;

namespace ShiftLifetimeCalculator.Application.Validators
{
    public class DaySheetValidator : AbstractValidator<DaySheet>
    {
        public DaySheetValidator()
        {
            RuleFor(x => x.Values)
                .NotEmpty()
                .ChildRules(x =>
                {
                    RuleFor(x => x.Values[Context.Duration])
                        .Must(duration => TimeSpan.TryParse(duration.RawValue, out var _))
                        .WithMessage("Invalid Duration field provided");

                    RuleFor(x => x.Values[Context.GrossIncome])
                        .Must(grossIncome => decimal.TryParse(grossIncome.RawValue, NumberStyles.Currency, CalculatorService.CurrencyFormat, out var _))
                        .WithMessage("Invalid GrossIncome field provided");

                    RuleFor(x => x.Values[Context.VatDeduction])
                        .Must(vatDeduction => decimal.TryParse(vatDeduction.RawValue, NumberStyles.Currency, CalculatorService.CurrencyFormat, out var _))
                        .WithMessage("Invalid VatDeduction field provided");

                    RuleFor(x => x.Values[Context.FuelExpense])
                        .Must(fuelExpense => decimal.TryParse(fuelExpense.RawValue, NumberStyles.Currency, CalculatorService.CurrencyFormat, out var _))
                        .WithMessage("Invalid FuelExpense field provided");

                    RuleFor(x => x.Values[Context.NetIncome])
                        .Must(netIncome => decimal.TryParse(netIncome.RawValue, NumberStyles.Currency, CalculatorService.CurrencyFormat, out var _))
                        .WithMessage("Invalid NetIncome field provided");

                    RuleFor(x => x.Values[Context.HourlyIncome])
                        .Must(hourlyIncome => decimal.TryParse(hourlyIncome.RawValue, NumberStyles.Currency, CalculatorService.CurrencyFormat, out var _))
                        .WithMessage("Invalid HourlyIncome field provided");

                    RuleFor(x => x.Values[Context.HourlyIncomePerPerson])
                        .Must(hourlyIncomePerPerson => decimal.TryParse(hourlyIncomePerPerson.RawValue, NumberStyles.Currency, CalculatorService.CurrencyFormat, out var _))
                        .WithMessage("Invalid HourlyIncomePerPerson field provided");
                });
        }
    }
}
