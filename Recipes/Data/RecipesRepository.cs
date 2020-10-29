namespace Recipes.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class RecipesRepository : IRecipesRepository
    {
        //ToDo inject my couch client
        //ToDo add new repository for couch, check dispose
        public RecipesRepository()
        {
            //ToDo repo in proiect separat
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            return RecipesFactory.GetRecipesWithBasicDetails();
        }

        public void AddRecipe(Recipe newRecipe)
        {
            RecipesFactory.AddRecipe(newRecipe);
        }

        public async Task<Recipe> GetRecipeById(string id)
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
