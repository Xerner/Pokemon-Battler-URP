using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace PokeBattler.Common
{
    public static class Http
    {
        private static HttpClient pokeApi = new HttpClient()
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
        };

        #region Generic URL stuff

        public static string Get(string url, Action<HttpResponseMessage> onError, Action<string> onSuccess)
        {
            return Get<string>(url, onError, onSuccess);
        }

        public static async Task<string> GetAsync(string url)
        {
            return await GetAsync<string>(url);
        }

        public static T Get<T>(string url, Action<HttpResponseMessage> onError, Action<T> onSuccess)
        {
            Debug2.Log("Fetching data from the web: " + url, LogLevel.All);
            Task<HttpResponseMessage> task = pokeApi.GetAsync(url);
            task.Wait();
            var response = task.Result;
            var processResponse = ProcessJsonResponseAsync<T>(response, onError);
            processResponse.Wait();
            var result = processResponse.Result;
            onSuccess(result);
            return result;
        }

        public static async Task<T> GetAsync<T>(string url)
        {
            Debug2.Log("Fetching string data from the web: " + url, LogLevel.All);
            var response = await pokeApi.GetAsync(url);
            var result = await ProcessJsonResponseAsync<T>(response);
            return result;
        }
    
        private static async Task<T> ProcessJsonResponseAsync<T>(HttpResponseMessage response, Action<HttpResponseMessage> onError = null)
        {
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                Debug2.Log("HTTP request failed: " + response.RequestMessage.RequestUri, LogLevel.All);
                onError?.Invoke(response);
                return default;
            }
            var str = await response.Content.ReadAsStringAsync();
            Debug2.Log("HTTP request succeeded: " + response.RequestMessage.RequestUri, LogLevel.All);
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(str, typeof(T));
            }
            var obj = JsonConvert.DeserializeObject<T>(str);
            return obj;
        }

        private static async Task<Texture2D> ProcessTexture2DResponseAsync(int width, int height, HttpResponseMessage response, Action<HttpResponseMessage> onError = null)
        {
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                Debug2.Log("HTTP request failed: " + response.RequestMessage.RequestUri, LogLevel.All);
                onError?.Invoke(response);
                return default;
            }
            var bytes = await response.Content.ReadAsByteArrayAsync();
            Debug2.Log("HTTP request succeeded: " + response.RequestMessage.RequestUri, LogLevel.All);
            var texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            return texture;
        }

        public static Texture2D GetTexture2D(int width, int height, string url, Action<HttpResponseMessage> onError, Action<Texture2D> onSuccess)
        {
            Debug2.Log("Fetching string data from the web: " + url, LogLevel.All);
            Task task = pokeApi.GetAsync(url);
            task.Wait();
            var response = pokeApi.GetAsync(url).Result;
            var processResponse = ProcessTexture2DResponseAsync(width, height, response, onError);
            processResponse.Wait();
            var result = processResponse.Result;
            onSuccess(result);
            return result;
        }

        public static async Task<Texture2D> GetTexture2DAsync(int width, int height, string url)
        {
            Debug2.Log("Fetching string data from the web: " + url, LogLevel.All);
            var response = await pokeApi.GetAsync(url);
            var result = await ProcessTexture2DResponseAsync(width, height, response);
            return result;
        }

        #endregion
    }
}
