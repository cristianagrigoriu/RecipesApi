using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recipes.Models;

namespace Recipes.Data
{
    public class RecipesRepository : IRecipesRepository
    {
        public RecipesRepository()
        {
        }

        public Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            return Task.Run(RecipesFactory.GetRecipesWithBasicDetails);
        }

        public void AddRecipe(Recipe newRecipe)
        {
            RecipesFactory.AddRecipe(newRecipe);
        }

        public Task<Recipe> GetRecipeById(string id)
        {
            return Task.Run(() => RecipesFactory.GetRecipeById(id));
        }

        public Task DeleteRecipe(string id)
        {
            return Task.Run(() => RecipesFactory.DeleteRecipe(id));
        }

        public void UpdateRecipe(Recipe updatedRecipe)
        {
            RecipesFactory.UpdateRecipe(updatedRecipe);
        }

        public Task<IEnumerable<Recipe>> GetRecipesByTime(double maxTime)
        {
            return Task.Run(() => RecipesFactory
                .GetRecipesWithBasicDetails()
                .Where(x => x.TimeInMinutes <= maxTime));
        }

        public Task<IEnumerable<Recipe>> GetRecipesByCategory(string category)
        {
            return Task.Run(() => RecipesFactory
                .GetRecipesWithBasicDetails()
                .Where(x => x.Category?.ToString() == category));
        }
    }
}
