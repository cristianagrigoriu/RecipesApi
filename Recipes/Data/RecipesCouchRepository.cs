﻿namespace Recipes.Data
{
    using System.Collections.Generic;

    public class RecipesCouchRepository : IRecipesRepository
    {
        private readonly ConnectionStrings connectionStrings;
        private string couchDbConnectionString;

        public RecipesCouchRepository()
        {
            //this.couchDbConnectionString = connectionStrings.CouchDb;
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            throw new System.NotImplementedException();
        }

        public void AddRecipe(Recipe newRecipe)
        {
            throw new System.NotImplementedException();
        }

        public Recipe GetRecipeById(string id)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteRecipe(string id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateRecipe(Recipe updatedRecipe)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetInstructionsOfRecipe(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}