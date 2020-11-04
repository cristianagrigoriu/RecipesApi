namespace Recipes.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using MyCouch;
    using MyCouch.Net;
    using MyCouch.Requests;
    using Newtonsoft.Json;

    public class RecipesCouchRepository : IRecipesRepository
    {
        private MyCouchStore store;

        public RecipesCouchRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            this.store = new MyCouchStore(connectionStrings.Value.CouchDb, "recipes");
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            var query = new Query("recipesByName", "byName")
            {
                IncludeDocs = true
            };
            var recipes = await this.store.QueryAsync<string, Recipe>(query);
            return recipes.Select(x => x.IncludedDoc);
        }

        public void AddRecipe(Recipe newRecipe)
        {
            this.store.StoreAsync(newRecipe).Wait();
        }

        public async Task<Recipe> GetRecipeById(string id)
        {
            return await this.store.GetByIdAsync<Recipe>(id);
        }

        public void DeleteRecipe(string id)
        {
            this.store.DeleteAsync(id).Wait();
        }

        public void UpdateRecipe(Recipe updatedRecipe)
        {
            var existingRecipe = store.GetHeaderAsync(updatedRecipe.Id).Result;
            updatedRecipe.Rev = existingRecipe.Rev;

            this.store.StoreAsync<Recipe>(updatedRecipe);
        }

        public List<string> GetInstructionsOfRecipe(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}