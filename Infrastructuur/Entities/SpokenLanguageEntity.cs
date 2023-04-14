using Newtonsoft.Json;

namespace Infrastructuur.Entities
{
    public class SpokenLanguageEntity
    {
        [JsonProperty("english_name")]
        public string EnglishName { get; set; }

        [JsonProperty("iso_639_1")]
        public string Iso6391 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

}
