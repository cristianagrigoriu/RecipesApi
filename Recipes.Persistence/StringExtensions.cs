namespace Recipes.Persistence
{
    public static class StringExtensions
    {
        public static string GetStringFormattedAsArray(this string[] input)
        {
            string queryString = "[";
            foreach (string word in input)
            {
                queryString += $"\"{word}\", ";
            }

            var lastComma = queryString.LastIndexOf(',');
            queryString = queryString.Remove(lastComma, 1);

            queryString = $"{queryString}]";

            return queryString;
        }

        public static string ToUpperFirstLetter(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            if (input.Length == 1)
            {
                return input.ToUpper();
            }

            return $"{char.ToUpper(input[0])}{input.Substring(1)}";
        }
    }
}
