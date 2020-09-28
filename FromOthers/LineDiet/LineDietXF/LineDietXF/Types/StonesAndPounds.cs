namespace LineDietXF.Types
{
    public class StonesAndPounds
    {
        public int Stones { get; set; }
        public decimal Pounds { get; set; }

        public StonesAndPounds(int stones, decimal pounds)
        {
            Stones = stones;
            Pounds = pounds;
        }
    }
}