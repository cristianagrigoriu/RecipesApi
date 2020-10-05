namespace Recipes.Data
{
    using System.Collections.Generic;

    public interface IRecipesRepository
    {
        IEnumerable<Recipe> GetAllRecipes();

        void AddRecipe(Recipe newRecipe);

        Recipe GetRecipeById(string id);
    }
}