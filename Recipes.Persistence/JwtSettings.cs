namespace Recipes.Persistence
{
    public class JwtSettings
    {
        public string BaseSecret { get; set; }

        public string Issuer { get; set; }

        public int ExpiryTimeInMinutes { get; set; }

        //ToDo Add Audience?
    }
}