namespace Recipes.Models
{
    using System.ComponentModel.DataAnnotations;
    using Domain;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class IngredientMeasureModel
    {
        [EnumDataType(typeof(UnitOfMeasure))]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public float Quantity { get; set; }
    }
}