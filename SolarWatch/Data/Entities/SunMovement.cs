namespace SolarWatch.Data.Entities
{
    public class SunMovement
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public DateTime Date { get; set; }
        public string SunRise { get; set; }
        public string Sunset { get; set; }

        public SunMovement() { }
        public SunMovement(int cityid, DateTime date, string sunRise, string sunSet)
        {
            CityId = cityid;
            Date = date;
            SunRise = sunRise;
            Sunset = sunSet;
        }
    }
}
