using Microsoft.Extensions.Logging;

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
        private readonly ILogger<RecipesCouchRepository> logger;
        private MyCouchStore store;

        //ToDo inject my couch client
        public RecipesCouchRepository(IOptions<ConnectionStrings> connectionStrings,
            LanguageService languageService,
            ILogger<RecipesCouchRepository> logger)
        {
            this.languageService = languageService;
            this.logger = logger;
            this.store = new MyCouchStore(connectionStrings.Value.CouchDb, "recipes");
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            logger.LogInformation("Logging from Couch Recipes Repository ❤💕👍");

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

                var response = await this.store.Client.Queries.FindAsync<Recipe>(request2);

                return response.Docs;
            }

            return await this.GetAllRecipes();
        }

        public async Task DeleteRecipe(string id)
        {
            await this.store.DeleteAsync(id);
        }

        public async void UpdateRecipe(Recipe updatedRecipe)
        {
            await this.store.StoreAsync<Recipe>(updatedRecipe);
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

            var response = await this.store.Client.Queries.FindAsync<Recipe>(request);

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

            var response = await this.store.Client.Queries.FindAsync<Recipe>(request);

            return response.Docs;
        }
    }
}