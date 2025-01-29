namespace SolarWatch.Models
{
    public record CityApiDto(string Name, float Longitude, float Latitude, string Country, string? State);
}
