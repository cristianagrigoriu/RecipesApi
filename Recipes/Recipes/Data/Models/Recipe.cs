using System.Collections.Generic;

namespace Recipes.Recipes.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BasicDetails { get; set; }

        public double TimeInMinutes { get; set; }

        public string Category { get; set; }

        //Instructions

        //categorie - mic dejun, ...
    }
}
