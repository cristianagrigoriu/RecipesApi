using System.Collections.Generic;

namespace Recipes.Models
{
    public class UserModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        //ToDo 1 add display name, add to token
        public IEnumerable<string> FavouriteRecipes { get; set; }
    }
}