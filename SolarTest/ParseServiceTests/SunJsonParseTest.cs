using SolarWatch.Services.ParseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarTest.ParseServiceTests
{
    [TestFixture]
    public class SunJsonParseTest
    {
        const string validJsonData = "{\"results\":{\"sunrise\":\"2:33:00 AM\",\"sunset\":\"2:42:10 PM\",\"solar_noon\":\"8:37:35 AM\",\"day_length\":\"12:09:10\",\"civil_twilight_begin\":\"2:11:43 AM\",\"civil_twilight_end\":\"3:03:26 PM\",\"nautical_twilight_begin\":\"1:45:36 AM\",\"nautical_twilight_end\":\"3:29:34 PM\",\"astronomical_twilight_begin\":\"1:19:22 AM\",\"astronomical_twilight_end\":\"3:55:48 PM\"},\"status\":\"OK\",\"tzid\":\"UTC\"}";
        const string validSunRise = "2:33:00 AM";
        const string validSunSet = "2:42:10 PM";

        public ISunJsonParser _service = new SunJsonParseService();

        [Test]
        public void ValidJson_ReturnsValidSunRise()
        {
            var result = _service.GetSunRise(validJsonData);
            Assert.AreEqual(validSunRise, result);
        }
        [Test]
        public void ValidJson_ReturnsValidSunSet()
        {
            var result = _service.GetSunSet(validJsonData);
            Assert.AreEqual(validSunSet, result);
        }
    }
}
