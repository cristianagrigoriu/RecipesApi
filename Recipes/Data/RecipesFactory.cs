using System.Collections.Generic;
using System.Linq;

namespace Recipes.Data
{
    public static class RecipesFactory
    {
        public static IEnumerable<Recipe> GetRecipesWithBasicDetails()
        {
            return Enumerable.Range(1, 5).Select(x => new Recipe
            {
                Id = x.ToString(),
                Name = $"Recipe {x}",
                BasicDetails = string.Concat(Enumerable.Repeat("bla", x))
            });
        }
    }
}
