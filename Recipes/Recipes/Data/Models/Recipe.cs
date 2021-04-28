using System.Collections.Generic;

namespace Recipes.Recipes.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string PreparationTime { get; set; }

        public string BakingTime { get; set; }

        public List<string> Ingredients { get; set; }

        public List<string> Instructions { get; set; }
    }
}
