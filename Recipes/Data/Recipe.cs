using System.Collections.Generic;

namespace Recipes.Data
{
    public class Recipe
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BasicDetails { get; set; }
        public double TimeInMinutes { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<string> Instructions { get; set; }
    }
}
