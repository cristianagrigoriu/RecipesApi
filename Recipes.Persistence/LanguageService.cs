namespace Recipes.Persistence
{
    public class LanguageService
    {
        public LanguageService(string language)
        {
            this.CurrentLanguage = language;
        }

        public string CurrentLanguage
        {
            get;
        }
    }
}
