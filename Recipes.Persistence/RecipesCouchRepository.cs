namespace Recipes.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.Extensions.Options;
    using MyCouch;
    using MyCouch.Requests;
    using Newtonsoft.Json.Linq;

    public class RecipesCouchRepository : IRecipesRepository
    {
        private readonly LanguageService languageService;
        private MyCouchStore store;

        public RecipesCouchRepository(IOptions<ConnectionStrings> connectionStrings,
            LanguageService languageService)
        {
            this.languageService = languageService;
            this.store = new MyCouchStore(connectionStrings.Value.CouchDb, "recipes");
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            var query = new Query("recipesByName", "byName")
            {
                IncludeDocs = true
            };
            var recipes = await this.store.QueryAsync<string, Recipe>(query);
            return recipes.Select(x => x.IncludedDoc);
        }

        public void AddRecipe(Recipe newRecipe)
        {
            var currentLanguage = this.languageService.CurrentLanguage;

            this.store.StoreAsync(newRecipe).Wait();
        }

        public async Task<Recipe> GetRecipeById(string id)
        {
            return await this.store.GetByIdAsync<Recipe>(id);
        }

        public async Task<IEnumerable<Recipe>> GetRecipeByIngredients(string[] ingredients)
        {
            if (ingredients.Any())
            {
                var ingredientsString = ingredients.GetStringFormattedAsArray();

                var request2 = new FindRequest().Configure(q =>
                    q.SelectorExpression
                    (
                        $"{{\"ingredientList\": {{\"$all\": {ingredientsString}}}}}"
                    )
                );

                var response = this.store.Client.Queries.FindAsync<Recipe>(request2).Result;

                return response.Docs;
            }

            return this.GetAllRecipes().Result;
        }

        public void DeleteRecipe(string id)
        {
            this.store.DeleteAsync(id).Wait();
        }

        public void UpdateRecipe(Recipe updatedRecipe)
        {
            this.store.StoreAsync<Recipe>(updatedRecipe);
        }

        public List<string> GetInstructionsOfRecipe(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByTime(double maxTime)
        {
            var selector = new JObject
            {
                {"timeInMinutes", new JObject{{"$lte", maxTime}}}
            };

            var request = new FindRequest()
                .Configure(x => x.SelectorExpression(selector.ToString()));

            var response = this.store.Client.Queries.FindAsync<Recipe>(request).Result;

            return response.Docs;
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByCategory(string category)
        {
            var categoryToFind = (Category)Enum.Parse(typeof(Category), category.ToUpperFirstLetter());

            var selector = new JObject
            {
                {"category",  categoryToFind.ToString().ToLower()}
            };

            var request = new FindRequest()
                .Configure(x => x.SelectorExpression(selector.ToString()));

            var response = this.store.Client.Queries.FindAsync<Recipe>(request).Result;

            return response.Docs;
        }
    }
}