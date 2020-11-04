namespace Recipes.Data
{
    using Microsoft.Extensions.Options;
    using MyCouch;

    public class IngredientsRepository : IIngredientsRepository
    {
        private MyCouchStore store;

        public IngredientsRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            this.store = new MyCouchStore(connectionStrings.Value.CouchDb, "recipes");
        }

        public void AddIngredient(RecipeIngredient newRecipeIngredient)
        {
            this.store.StoreAsync<RecipeIngredient>(newRecipeIngredient);
        }
    }
}
