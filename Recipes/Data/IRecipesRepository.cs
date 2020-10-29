namespace Recipes.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IRecipesRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipes();

        void AddRecipe(Recipe newRecipe);

        Task<Recipe> GetRecipeById(string id);

        void DeleteRecipe(string id);

        void UpdateRecipe(Recipe updatedRecipe);

        List<string> GetInstructionsOfRecipe(string id);
    }
}