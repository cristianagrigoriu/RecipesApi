using Recipes.Models;

namespace Recipes.Data
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Ingredient
    {
        [EnumDataType(typeof(BasicIngredient))]
        [JsonConverter(typeof(StringEnumConverter))]
        public BasicIngredient BasicIngredient { get; set; }
        public IngredientMeasure Measure { get; set; }
    }
}