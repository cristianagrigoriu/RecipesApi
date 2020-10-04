namespace Recipes.Data
{
    using System.Collections.Generic;

    public class RecipesRepository : IRecipesRepository
    {
        public IEnumerable<Recipe> GetAllRecipes()
        {
            return RecipesFactory.GetRecipesWithBasicDetails();
        }
    }
}
