﻿namespace Recipes.Persistence
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;

    public class RecipesRepository : IRecipesRepository
    {
        //ToDo inject my couch client
        public RecipesRepository()
        {
            //ToDo repo in proiect separat
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            return RecipesFactory.GetRecipesWithBasicDetails();
        }

        public void AddRecipe(Recipe newRecipe)
        {
            RecipesFactory.AddRecipe(newRecipe);
        }

        public async Task<Recipe> GetRecipeById(string id)
        {
            return RecipesFactory.GetRecipeById(id);
        }

        public async Task<IEnumerable<Recipe>> GetRecipeByIngredients(string[] ingredients)
        {
            return RecipesFactory
                .GetRecipesWithBasicDetails()
                .Where(x => x.Ingredients.Any(i => ingredients.Contains(i.Name)));
        }

        public void DeleteRecipe(string id)
        {
            RecipesFactory.DeleteRecipe(id);
        }

        public void UpdateRecipe(Recipe updatedRecipe)
        {
            RecipesFactory.UpdateRecipe(updatedRecipe);
        }

        public List<string> GetInstructionsOfRecipe(string id)
        {
            return RecipesFactory.GetInstructionsOfRecipe(id);
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByTime(double maxTime)
        {
            return RecipesFactory
                .GetRecipesWithBasicDetails()
                .Where(x => x.TimeInMinutes <= maxTime);
        }
    }
}