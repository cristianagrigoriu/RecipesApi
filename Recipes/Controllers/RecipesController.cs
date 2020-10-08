using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Recipes.Data;
using Recipes.Models;
using System;
using System.Linq;
using System.Net;

namespace Recipes.Controllers
{
    using Constants;

    //my couch
    [Route(MainRoutes.RecipesRoute)]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRecipesRepository recipesRepository;

        public RecipesController(
            IMapper mapper, 
            IRecipesRepository recipesRepository)
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

        ///<summary>
        ///Filter all recipes with a specified ingredient
        ///</summary>
        ///<param name="ingredient"></param>
        ///<returns></returns>
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

        ///<summary>
        ///Adds a new recipe to the existing ones
        ///</summary>
        ///<param name="newRecipe">
        ///A json specifying the fields of the new recipe
        ///</param>
        /// <response code="200">Successful operation</response>
        [HttpPost]
        public ActionResult<RecipeModel> AddRecipe(RecipeModel newRecipe)
        {
            var recipe = this.mapper.Map<Recipe>(newRecipe);
            this.recipesRepository.AddRecipe(recipe);

            return Created("", this.mapper.Map<RecipeModel>(recipe));

            //return BadRequest();
        }

        ///<summary>
        ///Updates an existing recipe by replacing it with the one sent
        ///</summary>
        ///<param name="id">
        ///Id of recipe to be updated
        ///</param>
        ///<param name="updatedRecipe">
        ///json of the recipe that will replace the existing one
        ///</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Recipe with the specified id not found</response>
        [HttpPut("{id}")]
        public ActionResult<RecipeModel> UpdateRecipe([FromRoute] string id, RecipeModel updatedRecipe)
        {
            var existingRecipe = this.recipesRepository.GetRecipeById(id);
            if (existingRecipe == null)
            {
                return NotFound($"Could not find recipe with id = {id}");
            }

            this.mapper.Map(updatedRecipe, existingRecipe);

            this.recipesRepository.UpdateRecipe(existingRecipe);

            return this.mapper.Map<RecipeModel>(existingRecipe);
        }

        /// <summary>
        /// Deletes a recipe with the given id
        /// </summary>
        /// <param name="id">
        ///Id of the recipe to be deleted
        /// </param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Recipe with the specified id not found</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(string id)
        {
            var recipeToBeDeleted = this.recipesRepository.GetRecipeById(id);
            if (recipeToBeDeleted == null)
            {
                return NotFound($"Could not find recipe with id = {id}");
            }

            this.recipesRepository.DeleteRecipe(id);

            return Ok();
        }
    }
}