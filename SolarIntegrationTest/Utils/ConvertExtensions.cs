using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SolarIntegrationTest.Utils
{
    public static class ConvertExtensions
    {
        public static HttpContent ToHttpContent<T>(T data)
        {
            string jsonContent = JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
