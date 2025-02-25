using SolarWatch.Data.Entities;

namespace SolarWatch.Data.DataImport
{
    public static class CityNameReader
    {
        public static List<CityName> Read()
        {
            List<CityName> cityNames = new();
            int Id = 1;

            string _csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "city-raw-data", "citynames.csv");
            using (StreamReader reader = new StreamReader(_csvFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    cityNames.Add(new CityName { Id = Id, Name = line });
                    Id++;
                }
            }
            return cityNames;
        }
    }
}
