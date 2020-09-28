namespace Common.MyExtensions
{
    public static class IntExtension
    {
        public static string LeadingZeros(this int value, int n)
        {
            return value.ToString().PadLeft(n, '0');
        }
    }
}
