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

        public RecipesController(IMapper mapper)
        {
            this.mapper = mapper;
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
            try
            {
                var foundRecipes = RecipesFactory.GetRecipesWithBasicDetails();
                var recipeModels = mapper.Map<RecipeModel[]>(foundRecipes);
                return recipeModels;
            }
            catch (System.Exception e)
            {
                return this.StatusCode(500);
            }

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
        public ActionResult<RecipeModel> GetRecipe(string id)
        {
            try
            {
                var foundRecipe = RecipesFactory
                    .GetRecipesWithBasicDetails()
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (foundRecipe == null)
                {
                    return NotFound();
                }

                return mapper.Map<RecipeModel>(foundRecipe);
            }
            catch (System.Exception)
            {
                return this.StatusCode(500);
            }
        }

        [HttpGet("search")]
        public ActionResult<RecipeModel[]> GetRecipeByIngredient(string ingredient)
        {
            try
            {
                Enum.TryParse(ingredient.ToUpper(), out BasicIngredient basicIngredient);

                //var x = basicIngredient == BasicIngredient.Tomatoes;

                var allRecipes = RecipesFactory
                    .GetRecipesWithBasicDetails();

                var foundRecipes = new List<Recipe>();

                foreach(Recipe recipe in allRecipes)
                {
                    if (recipe.Ingredients == null)
                    {
                        continue;
                    }
                    foreach (Ingredient recipeIngredient in recipe.Ingredients)
                    {
                        if (recipeIngredient.BasicIngredient == basicIngredient)
                        {
                            //yield
                            foundRecipes.Add(recipe);
                            continue;
                        }
                    }
                }

                    //.Where(x => x.Ingredients
                    //    .Any(i => i.BasicIngredient == basicIngredient));

                if (!foundRecipes.Any())
                {
                    return NotFound();
                }

                return this.mapper.Map<RecipeModel[]>(foundRecipes);
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }
    }
}