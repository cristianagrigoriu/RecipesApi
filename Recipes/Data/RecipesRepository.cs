namespace Recipes.Data
{
    using System.Collections.Generic;

    public class RecipesRepository : IRecipesRepository
    {
        public IEnumerable<Recipe> GetAllRecipes()
        {
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
    }
}
