using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo
{
    // WebClient is old type; HttpClient shall be preferred.
    class WebDemo
    {
        public static void Demo()
        {
            var webDemo = new WebDemo();
            webDemo.HttpClient_GetStringAsync_Synchronously();
        }

        public void HttpClient_GetStringAsync_Synchronously()
        {
            var httpClient = new HttpClient();
            var url = "http://freegeoip.net/json";
            Console.WriteLine($"Sending GET request to {url}...");
            var task = httpClient.GetStringAsync(url);
            var data = task.Result;
            Console.WriteLine($"Response from {url}:{Environment.NewLine}{data}");
        }
    }
}