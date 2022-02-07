using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FunctionalTests.Extensions
{
    public static class JsonConverterExeptions
    {
        public static async Task<T> ReadAsync<T>(this HttpResponseMessage response) where T : class
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }

        public static async Task<List<T>> ReadListAsync<T>(this HttpResponseMessage response) where T : class
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(result);
        }
    }
}
