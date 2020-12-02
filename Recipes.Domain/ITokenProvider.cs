namespace Recipes.Domain
{
    public interface ITokenProvider
    {
        string GenerateToken(string username);
    }
}