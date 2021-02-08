using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Recipes.Domain
{
    public class Recipe
    {
        private List<RecipeIngredient> ingredients = new List<RecipeIngredient>();

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_rev")]
        public string Rev { get; set; }

        public string Name { get; set; }

        public string BasicDetails { get; set; }

        public double TimeInMinutes { get; set; }

        public Category Category { get; set; }

        public List<RecipeIngredient> Ingredients
        {
            get => this.ingredients;
            set => this.ingredients = value?.ToList() ?? new List<RecipeIngredient>();
        }

        //ToDo differences in collections
        public IEnumerable<string> IngredientList => this.Ingredients.Select(x => x.Name).ToList();

        public List<string> Instructions { get; set; }
    }
}
