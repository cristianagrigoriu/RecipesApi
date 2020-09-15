﻿using AutoMapper;
using Recipes.Models;

namespace Recipes.Data
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Recipe, RecipeModel>();
        }
    }
}
