using SolarWatch.Services.ParseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarTest.ParseServiceTests
{
    [TestFixture]
    public class CityJsonParseTest
    {
        const string validJsonData = "[{\"name\":\"London\",\"local_names\":{\"ab\":\"Лондон\",\"nv\":\"Tooh Dineʼé Bikin Haalʼá\",\"sk\":\"Londýn\",\"is\":\"London\",\"et\":\"London\",\"bg\":\"Лондон\",\"cy\":\"Llundain\",\"hr\":\"London\",\"bo\":\"ལོན་ཊོན།\",\"gl\":\"Londres\",\"vi\":\"Luân Đôn\",\"ku\":\"London\",\"ky\":\"Лондон\",\"no\":\"London\",\"or\":\"ଲଣ୍ଡନ\",\"ps\":\"لندن\",\"mg\":\"Lôndôna\",\"vo\":\"London\",\"gd\":\"Lunnainn\",\"li\":\"Londe\",\"km\":\"ឡុងដ៍\",\"ru\":\"Лондон\",\"el\":\"Λονδίνο\",\"pt\":\"Londres\",\"ro\":\"Londra\",\"kk\":\"Лондон\",\"tw\":\"London\",\"mr\":\"लंडन\",\"lb\":\"London\",\"ff\":\"London\",\"ny\":\"London\",\"nl\":\"Londen\",\"fo\":\"London\",\"de\":\"London\",\"ln\":\"Lóndɛlɛ\",\"br\":\"Londrez\",\"ht\":\"Lonn\",\"lv\":\"Londona\",\"io\":\"London\",\"sl\":\"London\",\"an\":\"Londres\",\"ia\":\"London\",\"bn\":\"লন্ডন\",\"jv\":\"London\",\"id\":\"London\",\"av\":\"Лондон\",\"wa\":\"Londe\",\"cu\":\"Лондонъ\",\"cs\":\"Londýn\",\"bh\":\"लंदन\",\"nn\":\"London\",\"cv\":\"Лондон\",\"wo\":\"Londar\",\"rm\":\"Londra\",\"mi\":\"Rānana\",\"ur\":\"علاقہ لندن\",\"ca\":\"Londres\",\"hu\":\"London\",\"es\":\"Londres\",\"kl\":\"London\",\"am\":\"ለንደን\",\"zh\":\"伦敦\",\"fy\":\"Londen\",\"ig\":\"London\",\"bi\":\"London\",\"my\":\"လန်ဒန်မြို့\",\"os\":\"Лондон\",\"ie\":\"London\",\"sw\":\"London\",\"fj\":\"Lodoni\",\"it\":\"Londra\",\"sc\":\"Londra\",\"fa\":\"لندن\",\"be\":\"Лондан\",\"gu\":\"લંડન\",\"tl\":\"Londres\",\"uz\":\"London\",\"sm\":\"Lonetona\",\"en\":\"London\",\"yi\":\"לאנדאן\",\"da\":\"London\",\"ascii\":\"London\",\"sv\":\"London\",\"hy\":\"Լոնդոն\",\"he\":\"לונדון\",\"kn\":\"ಲಂಡನ್\",\"so\":\"London\",\"gn\":\"Lóndyre\",\"mn\":\"Лондон\",\"si\":\"ලන්ඩන්\",\"ce\":\"Лондон\",\"sq\":\"Londra\",\"lt\":\"Londonas\",\"mt\":\"Londra\",\"tr\":\"Londra\",\"gv\":\"Lunnin\",\"ms\":\"London\",\"ml\":\"ലണ്ടൻ\",\"eo\":\"Londono\",\"sd\":\"لنڊن\",\"ka\":\"ლონდონი\",\"yo\":\"Lọndọnu\",\"tg\":\"Лондон\",\"kv\":\"Лондон\",\"pa\":\"ਲੰਡਨ\",\"ne\":\"लन्डन\",\"ko\":\"런던\",\"af\":\"Londen\",\"te\":\"లండన్\",\"ta\":\"இலண்டன்\",\"na\":\"London\",\"ar\":\"لندن\",\"tt\":\"Лондон\",\"zu\":\"ILondon\",\"eu\":\"Londres\",\"uk\":\"Лондон\",\"sa\":\"लन्डन्\",\"to\":\"Lonitoni\",\"sh\":\"London\",\"bm\":\"London\",\"ja\":\"ロンドン\",\"ga\":\"Londain\",\"st\":\"London\",\"om\":\"Landan\",\"qu\":\"London\",\"bs\":\"London\",\"feature_name\":\"London\",\"th\":\"ลอนดอน\",\"ug\":\"لوندۇن\",\"tk\":\"London\",\"kw\":\"Loundres\",\"co\":\"Londra\",\"sn\":\"London\",\"fi\":\"Lontoo\",\"sr\":\"Лондон\",\"su\":\"London\",\"mk\":\"Лондон\",\"ba\":\"Лондон\",\"oc\":\"Londres\",\"lo\":\"ລອນດອນ\",\"pl\":\"Londyn\",\"az\":\"London\",\"hi\":\"लंदन\",\"fr\":\"Londres\",\"ay\":\"London\",\"ee\":\"London\",\"ha\":\"Landan\",\"se\":\"London\"},\"lat\":51.5073219,\"lon\":-0.1276474,\"country\":\"GB\",\"state\":\"England\"}]";
        public ICityJsonParser _service = new CityJsonParseService();
        const string validName = "London";
        const string validCountry = "GB";
        const float validLongitude = -0.1276474f;
        const float validLatitude = 51.5073219f;
        const string validState = "England";


        [Test]
        public void ValidJson_ReturnsValidCity()
        {
            var result = _service.GetCityData(validJsonData);

            Assert.That(result.Name, Is.EqualTo(validName));
            Assert.That(result.Latitude, Is.EqualTo(validLatitude));
            Assert.That(result.Longitude, Is.EqualTo(validLongitude));
            Assert.That(result.Country, Is.EqualTo(validCountry));
            Assert.That(result.State, Is.EqualTo(validState));
        }


        [Test]
        public void InvalidJson_ReturnsValidCity()
        {
            Assert.Throws<Exception>(() => _service.GetCityData("invalidJsonData"));
        }
    }
}
