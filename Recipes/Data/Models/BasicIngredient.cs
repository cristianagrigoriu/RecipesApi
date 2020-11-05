namespace Recipes.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [JsonConverter(typeof(StringEnumConverter))]
    public enum BasicIngredient
    {
        [EnumMember(Value = "tomatoes")]
        TOMATOES = 1,
        [EnumMember(Value = "eggs")]
        EGGS,
        [EnumMember(Value = "pasta")]
        PASTA,
        [EnumMember(Value = "rice")]
        RICE
    }
}