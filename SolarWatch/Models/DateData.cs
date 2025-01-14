namespace SolarWatch.Models
{
    public class DateData
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public string ToString()
        {
            return $"{Year}-{Month}-{Day}";
        }
    }
}
