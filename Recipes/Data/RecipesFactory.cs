using Recipes.Models;
using System.Collections.Generic;

namespace Recipes.Data
{
    using System.Linq;

    public static class RecipesFactory
    {
        private static IEnumerable<Recipe> allRecipes = CreateRecipes();

        public static IEnumerable<Recipe> GetRecipesWithBasicDetails() => allRecipes;

        public static void AddRecipe(Recipe newRecipe)
        {
            var newId = GetMaximumExistingId() + 1;
            newRecipe.Id = newId.ToString();
            allRecipes = allRecipes.Append(newRecipe);
        }

        private static IEnumerable<Recipe> CreateRecipes()
        {
            return new List<Recipe>
            {
                new Recipe
                {
                    Id = "1",
                    Name = "Banana Pancakes",
                    BasicDetails = "Easy breakfast"
                },
                new Recipe
                {
                    Id = "2",
                    Name = "Zuchinni Bread",
                    BasicDetails = "Get the food processor ready"
                },
                new Recipe
                {
                    Id = "3",
                    Name = "Homemade icecream",
                    BasicDetails = "For lazy weekends"
                },
                new Recipe
                {
                    Id = "4",
                    Name = "Pizza",
                    BasicDetails = "Quick and delicious"
                },
                new Recipe
                {
                    Id = "5",
                    Name = "Tomato Soup",
                    BasicDetails = "Fall is coming",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient
                        {
                            BasicIngredient = BasicIngredient.TOMATOES,
                            QuantityInGrams = 200
                        }
                    }
                }
            };
        }

        private static int GetMaximumExistingId() => allRecipes.Max(x => int.Parse(x.Id));

        public static Recipe GetRecipeById(string id)
        {
            return allRecipes.FirstOrDefault(x => x.Id == id);
        }
    }
}
