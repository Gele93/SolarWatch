namespace SolarWatch.Services
{
    public interface ISunJsonParser
    {
        TimeOnly GetSunRise(string sunData);
        TimeOnly GetSunSet(string sunData);
    }
}
