namespace Recipes.Domain
{
    public interface IHashGenerator
    {
        string GenerateHashFor(string input);
    }
}