using Newtonsoft.Json;

namespace Infrastructuur.Entities
{
    public class GenreEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
