using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiTest
{
  class Program
  {
    public class Article
    {
      public string Section { get; set; }
      public string Title { get; set; }
      public string Abstract { get; set; }
      public string Url { get; set; }
      public string Byline { get; set; }
    }

    static void Main(string[] args)
    {
      var apiCallTask = ApiHelper.ApiCall(EnvironmentVariables.ApiKey);
      var result = apiCallTask.Result;
      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      Console.WriteLine(jsonResponse["results"]);
    }

    class ApiHelper
    {
      public static async Task<string> ApiCall(string apiKey)
      {
        RestClient client = new RestClient("https://api.nytimes.com/svc/topstories/v2");
        RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.GET);
        var response = await client.ExecuteTaskAsync(request);
        return response.Content;
      }
    }
  }
}
