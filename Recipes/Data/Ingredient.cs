using Recipes.Models;

namespace Recipes.Data
{
    public class Ingredient
    {
        public BasicIngredient BasicIngredient { get; set; }
        public double QuantityInGrams { get; set; }
    }
}