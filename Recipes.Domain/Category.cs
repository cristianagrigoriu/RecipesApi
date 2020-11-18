namespace Recipes.Domain
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Category
    {
        [EnumMember(Value = "unknown")]
        Unknown = 0,
        [EnumMember(Value = "breakfast")]
        Breakfast = 1,
        [EnumMember(Value = "lunch")]
        Lunch = 2,
        [EnumMember(Value = "dessert")]
        Dessert = 3
    }
}