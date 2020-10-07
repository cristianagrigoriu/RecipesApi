namespace Recipes.Data
{
    using System.Collections.Generic;
    using Models;

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
