using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Recipes.Data;
using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Recipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRecipesRepository recipesRepository;

        public RecipesController(IMapper mapper, IRecipesRepository recipesRepository)
        {
            this.mapper = mapper;
            this.recipesRepository = recipesRepository;
        }

        ///<summary>
        ///Get all recipes
        ///</summary>
        /// <response code="200">Successful operation</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(RecipeModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesErrorResponseType(typeof(RecipeModel))]
        public ActionResult<RecipeModel[]> GetAllRecipes()
        {
            var foundRecipes = this.recipesRepository.GetAllRecipes();
            var recipeModels = mapper.Map<RecipeModel[]>(foundRecipes);
            return recipeModels;
        }

        ///<summary>
        ///Get a recipe by the id
        ///</summary>
        ///<param name="id">
        ///The id of the recipe
        ///</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid id supplied</response>
        /// <response code="404">Recipe with the specified id not found</response>
        /// <response code="500">Server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RecipeModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesErrorResponseType(typeof(RecipeModel))]
        public ActionResult<RecipeModel> GetRecipe([FromRoute] string id)
        {
            var foundRecipe = this.recipesRepository
                .GetAllRecipes()
                .FirstOrDefault(x => x.Id == id);

            if (foundRecipe == null)
            {
                return NotFound();
            }

            return mapper.Map<RecipeModel>(foundRecipe);
        }

        /// <summary>
        /// Filter all recipes with a specified ingredient
        /// </summary>
        /// <param name="ingredient"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public ActionResult<RecipeModel[]> GetRecipeByIngredient([FromQuery] string ingredient)
        {
            Enum.TryParse(ingredient.ToUpper(), out BasicIngredient basicIngredient);

            var foundRecipes = this.recipesRepository
                .GetAllRecipes()
                .Where(x => x.Ingredients
                    .Any(y => y.BasicIngredient == basicIngredient));

            return this.mapper.Map<RecipeModel[]>(foundRecipes);
        }
    }
}