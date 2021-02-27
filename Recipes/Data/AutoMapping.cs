using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using AutoMapper.Internal;
using Recipes.Models;

namespace Recipes.Data
{
    using Domain;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Recipe, RecipeModel>();
            CreateMap<RecipeModel, Recipe>().ForMember(x => x.Id, opt => opt.Ignore());
                //.ForMember(x => x.IngredientList.ForAll(m => m.)); //ToDo research

            CreateMap<RecipeIngredient, RecipeIngredientModel>();
            CreateMap<RecipeIngredientModel, RecipeIngredient>();
            
            CreateMap<IngredientMeasureModel, IngredientMeasure>();
            CreateMap<IngredientMeasure, IngredientMeasureModel>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            CreateMap<User, LoginUserModel>();
            CreateMap<LoginUserModel, User>();
        }
    }
}
