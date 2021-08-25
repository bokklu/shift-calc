using OfficeOpenXml;
using ShiftLifetimeCalculator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShiftLifetimeCalculator.IO
{
    public class WorkbookRepository : IDisposable
    {
        private readonly ExcelPackage _excelPackage;

        public WorkbookRepository(string path)
        {
            _excelPackage = new ExcelPackage(new FileInfo(path));
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public void Dispose()
        {
            _excelPackage?.Dispose();
        }

        public IEnumerable<DaySheet> Read()
        {
            var daySheets = new List<DaySheet>();
            var worksheets = _excelPackage.Workbook.Worksheets.Skip(2);

            foreach (var worksheet in worksheets)
            {
                var daySheet = DaySheet.Create();

                foreach (var daySheetValue in daySheet.Values)
                {
                    if (daySheetValue.Key == Context.Duration)
                    {
                        var durationString = worksheet.Cells[daySheetValue.Value.Row, daySheetValue.Value.Column].Value.ToString();
                        daySheetValue.Value.SetRawValue(durationString.Substring(durationString.LastIndexOf(' ') + 1));
                    }
                    else
                    {
                        daySheetValue.Value.SetRawValue(worksheet.Cells[daySheetValue.Value.Row, daySheetValue.Value.Column].Value.ToString());
                    }
                }

                daySheets.Add(daySheet);
            }

            return daySheets;
        }

        public void Write(LifetimeSheet lifetimeSheet)
        {
            var worksheet = _excelPackage.Workbook.Worksheets[0];

            foreach (var lifetimeSheetValue in lifetimeSheet.Values)
            {
                worksheet.Cells[lifetimeSheetValue.Value.Row, lifetimeSheetValue.Value.Column].Value = lifetimeSheetValue.Value.RawValue;
                worksheet.Cells[lifetimeSheetValue.Value.Row, lifetimeSheetValue.Value.Column].Style.Font.Bold = true;
                worksheet.Cells[lifetimeSheetValue.Value.Row, lifetimeSheetValue.Value.Column].Style.Font.UnderLine = true;
                worksheet.Cells[lifetimeSheetValue.Value.Row, lifetimeSheetValue.Value.Column].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells[lifetimeSheetValue.Value.Row, lifetimeSheetValue.Value.Column].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkSeaGreen);
            }

            _excelPackage.Save();
        }
    }
}
