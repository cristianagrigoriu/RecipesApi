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
            CreateMap<RecipeModel, Recipe>().
                ForMember(x => x.Id, opt => opt.Ignore()); //ToDo research

            CreateMap<RecipeIngredient, RecipeIngredientModel>();
            CreateMap<RecipeIngredientModel, RecipeIngredient>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            CreateMap<User, LoginUserModel>();
            CreateMap<LoginUserModel, User>();
        }
    }
}
