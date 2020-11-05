using System.Collections.Generic;

namespace Recipes.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RecipeModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BasicDetails { get; set; }

        public double TimeInMinutes { get; set; }

        public List<RecipeIngredientModel> Ingredients { get; set; }

        //Instructions

        //categorie - mic dejun, ...
    }
}
