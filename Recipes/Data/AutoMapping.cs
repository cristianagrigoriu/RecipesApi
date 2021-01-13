using AutoMapper;
using Recipes.Models;

namespace Recipes.Data
{
    using Domain;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Recipe, RecipeModel>();
            CreateMap<RecipeModel, Recipe>();

            CreateMap<RecipeIngredient, RecipeIngredientModel>();
            CreateMap<RecipeIngredientModel, RecipeIngredient>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }
    }
}
