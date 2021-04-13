namespace Recipes.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Constants;
    using Data;
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route(MainRoutes.IngredientsRoute)]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientsRepository ingredientsRepository;
        private readonly IMapper mapper;

        public IngredientsController(
            IIngredientsRepository ingredientsRepository, 
            IMapper mapper)
        {
            this.ingredientsRepository = ingredientsRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public ActionResult<RecipeIngredientModel> AddIngredient(RecipeIngredientModel newIngredient)
        {
            var ingredient = this.mapper.Map<RecipeIngredient>(newIngredient);
            this.ingredientsRepository.AddIngredient(ingredient);

            return Created("", this.mapper.Map<RecipeIngredientModel>(ingredient));
        }

        public void ScaleIngredients() { }

        //[HttpGet]
        //public ActionResult<List<string>> Get(string id)
        //{
        //    var recipe = this.ingredientsRepository.GetRecipeById(id);

        //    if (recipe == null)
        //    {
        //        return NotFound();
        //    }

        //    var instructions = this.ingredientsRepository.GetInstructionsOfRecipe(id);

        //    return instructions;
        //}
    }
}
