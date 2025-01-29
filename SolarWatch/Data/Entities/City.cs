namespace SolarWatch.Data.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Country { get; set; }
        public string? State { get; set; }
        public List<SunMovement> SunMovements { get; set; }
    }
}
