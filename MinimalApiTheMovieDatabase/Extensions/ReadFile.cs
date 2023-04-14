using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MinimalApiTheMovieDatabase.Extensions
{
    public static class ReadFile
    {

        public static ApiKey GetApiKey(this IConfiguration config)
        {
            string apiKeyPath = config.GetValue<string>("apiPath");
            if (!File.Exists(apiKeyPath))
            {
                throw new Exception("File not found!");
            }
            string file = File.ReadAllText(apiKeyPath);
       
            return JsonConvert.DeserializeObject<ApiKey>(file); 
        }
    }
    public class ApiKey
    {
        [JsonProperty("apiKey")]
        public string Key { get; set; }
    }
}
