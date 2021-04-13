using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Models;

namespace Recipes.Data
{
    public interface IRecipesRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipes();

        void AddRecipe(Recipe newRecipe);

        Task<Recipe> GetRecipeById(string id);

        Task DeleteRecipe(string id);

        void UpdateRecipe(Recipe updatedRecipe);

        Task<IEnumerable<Recipe>> GetRecipesByTime(double maxTime);

        Task<IEnumerable<Recipe>> GetRecipesByCategory(string category);
    }
}