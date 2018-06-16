using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace WebApplication1.Helpers
{
    public static class WebRequests
    {
        private static readonly HttpClient Client = new HttpClient();

        public static T PostRequest<T>(string url, T value) =>
            DeserializeResponse<T>(Client.PostAsync(url, new FormUrlEncodedContent(value.ToDictionary())).Result);

        public static T GetRequest<T>(string url) =>
            DeserializeResponse<T>(Client.GetAsync(url).Result);

        public static T PutRequest<T>(string url, T value) =>
            DeserializeResponse<T>(Client.PutAsync(url, new FormUrlEncodedContent(value.ToDictionary())).Result);

        public static HttpStatusCode DeleteRequest(string url) =>
            Client.DeleteAsync(url).Result.StatusCode;

        private static T DeserializeResponse<T>(HttpResponseMessage response) =>
            JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
    }
}
