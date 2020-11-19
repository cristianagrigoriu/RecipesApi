namespace Recipes.Persistence
{
    using System;

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
