using Recipes.Models;

namespace Recipes.Data
{
    public class Ingredient
    {
        public string Id { get; set; }

        public string Rev { get; set; }

        public string Name { get; set; }

        public IngredientMeasure Measure { get; set; }
    }
}