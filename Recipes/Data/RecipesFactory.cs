using Recipes.Models;
using System.Collections.Generic;

namespace Recipes.Data
{
    public static class RecipesFactory
    {
        public static IEnumerable<Recipe> GetRecipesWithBasicDetails()
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
    }
}
