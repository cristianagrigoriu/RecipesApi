namespace Recipes.Domain
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
        Grams = 1,
        [EnumMember(Value = "pieces")]
        Pieces = 2,
        [EnumMember(Value = "liter")]
        Liter = 3,
        [EnumMember(Value = "cups")] //spoons, too?
        Cups = 4
    }
}