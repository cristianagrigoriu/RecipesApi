namespace Recipes.Data
{
    using System;
    using System.Collections.Generic;
    using Models;
    using MyCouch;

    public class RecipesRepository : IRecipesRepository
    {
        private MyCouchStore store;

        //ToDo inject my couch client
        //ToDo add new repository for couch, check dispose
        public RecipesRepository()
        {
            //ToDo repo in proiect separat
            //ToDo URL put in app settings
            this.store = new MyCouchStore("http://admin:admin1@localhost:5984", "recipes");
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            //ToDo get all -> not supported in couch db; use views
            return RecipesFactory.GetRecipesWithBasicDetails();
        }

        public void AddRecipe(Recipe newRecipe)
        {
            RecipesFactory.AddRecipe(newRecipe);
        }

        public Recipe GetRecipeById(string id)
        {
            return RecipesFactory.GetRecipeById(id);
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
    }
}
