namespace Recipes.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [JsonConverter(typeof(StringEnumConverter))]
    public enum UnitOfMeasure
    {
        [EnumMember(Value = "unknown")]
        Unknown = 0,
        [EnumMember(Value = "grams")]
        Grams,
        [EnumMember(Value = "pieces")]
        Pieces,
        [EnumMember(Value = "liter")]
        Liter
    }
}