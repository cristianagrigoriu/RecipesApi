namespace Recipes.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRecipesRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipes();

        void AddRecipe(Recipe newRecipe);

        Task<Recipe> GetRecipeById(string id);

        Task<IEnumerable<Recipe>> GetRecipeByIngredients(string[] ingredients);

        void DeleteRecipe(string id);

        void UpdateRecipe(Recipe updatedRecipe);

        List<string> GetInstructionsOfRecipe(string id);

        Task<IEnumerable<Recipe>> GetRecipesByTime(double maxTime);
    }
}