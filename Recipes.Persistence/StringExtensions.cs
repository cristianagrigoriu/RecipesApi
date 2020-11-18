namespace Recipes.Persistence
{
    public static class StringExtensions
    {
        public static string GetStringFormattedAsArray(this string[] input)
        {
            string iString = "[";
            foreach (string word in input)
            {
                iString += $"\"{word}\", ";
            }

            var lastComma = iString.LastIndexOf(',');
            iString = iString.Remove(lastComma, 1);

            iString = $"{iString}]";

            return iString;
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
