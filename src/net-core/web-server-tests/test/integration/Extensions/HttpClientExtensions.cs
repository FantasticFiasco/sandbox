using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Integration.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient self,
            string url,
            T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);

            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return self.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(
            this HttpClient self,
            string url,
            T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);

            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return self.PutAsync(url, content);
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent self)
        {
            var dataAsString = await self.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(dataAsString);
        }
    }
}
