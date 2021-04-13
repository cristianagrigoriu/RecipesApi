﻿using System.Collections.Generic;
using System.Linq;
using Recipes.Models;

namespace Recipes.Data
{
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

        private static IEnumerable<Recipe> CreateRecipes() =>
            new List<Recipe>
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
                        BasicDetails = "Fall is coming"
                    }
                };

        public static Recipe GetRecipeById(string id)
        {
            return allRecipes.FirstOrDefault(x => x.Id == id);
        }

        public static void DeleteRecipe(string id)
        {
            allRecipes = allRecipes.Where(x => x.Id != id);
        }

        public static void UpdateRecipe(Recipe updatedRecipe)
        {
            allRecipes = allRecipes.Where(x => x.Id != updatedRecipe.Id);
            allRecipes = allRecipes.Append(updatedRecipe).OrderBy(x => int.Parse(x.Id));
        }

        private static int GetMaximumExistingId() => allRecipes.Max(x => int.Parse(x.Id));
    }
}