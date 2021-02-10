using System.Collections.Generic;
using Newtonsoft.Json;

namespace Recipes.Domain
{
    public class User
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_rev")]
        public string Rev { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public IEnumerable<string> FavouriteRecipes { get; set; } = new List<string>();
    }
}