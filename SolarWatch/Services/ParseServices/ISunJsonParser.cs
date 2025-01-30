namespace SolarWatch.Services.ParseServices
{
    public interface ISunJsonParser
    {
        string GetSunRise(string sunData);
        string GetSunSet(string sunData);
    }
}
