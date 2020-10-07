namespace Recipes.Data
{
    using System.Collections.Generic;
    using Models;

    public interface IRecipesRepository
    {
        IEnumerable<Recipe> GetAllRecipes();

        void AddRecipe(Recipe newRecipe);

        Recipe GetRecipeById(string id);

        void DeleteRecipe(string id);

        void UpdateRecipe(Recipe updatedRecipe);

        List<string> GetInstructionsOfRecipe(string id);
    }
}