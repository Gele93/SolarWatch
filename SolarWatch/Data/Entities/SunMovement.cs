namespace SolarWatch.Data.Entities
{
    public class SunMovement
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public DateTime Date { get; set; }
        public DateTime SunRise { get; set; }
        public DateTime Sunset { get; set; }
    }
}
