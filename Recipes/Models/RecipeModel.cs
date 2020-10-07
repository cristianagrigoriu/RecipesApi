using System.Collections.Generic;

namespace Recipes.Models
{
    public class RecipeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BasicDetails { get; set; }
        public double TimeInMinutes { get; set; }
        public List<IngredientModel> Ingredients { get; set; }
    }
}
