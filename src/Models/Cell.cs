namespace ShiftLifetimeCalculator.Models
{
    public class Cell
    {
        public int Column { get; }
        public int Row { get; }
        public string RawValue { get; private set; }

        private Cell(int column, int row, string rawValue)
        {
            Column = column;
            Row = row;
            RawValue = rawValue;
        }

        public static Cell Create(int column, int row) => new Cell(column, row, default);

        public void SetRawValue(string rawValue)
        {
            RawValue = rawValue;
        }
    }
}
