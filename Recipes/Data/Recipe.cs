using System.Collections.Generic;

namespace Recipes.Data
{
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Recipe
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BasicDetails { get; set; }
        public double TimeInMinutes { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public BasicIngredient SpecialIngredient => BasicIngredient.PASTA;
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<string> Instructions { get; set; }
    }
}
