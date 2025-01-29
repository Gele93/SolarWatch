namespace SolarWatch.Services.ParseServices
{
    public interface ISunJsonParser
    {
        DateTime GetSunRise(string sunData);
        DateTime GetSunSet(string sunData);
    }
}
