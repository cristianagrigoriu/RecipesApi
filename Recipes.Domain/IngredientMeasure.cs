namespace Recipes.Domain
{
    //using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class IngredientMeasure
    {
        //[EnumDataType(typeof(UnitOfMeasure))]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public float Quantity { get; set; }
    }
}