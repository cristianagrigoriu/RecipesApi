﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Recipes.Models;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Recipes.Controllers
{
    using System.Threading.Tasks;
    using Constants;
    using Domain;

    [Route(MainRoutes.RecipesRoute)]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRecipesRepository recipesRepository;
        private readonly ILogger<RecipesController> logger;

        public RecipesController(
            IMapper mapper, 
            IRecipesRepository recipesRepository,
            ILogger<RecipesController> logger)
        {
            this.mapper = mapper;
            this.recipesRepository = recipesRepository;
            this.logger = logger;
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
        public async Task<ActionResult<RecipeModel[]>> GetAllRecipes()
        {
            this.logger.LogInformation("Logging from Recipes Controller ❤");
            var foundRecipes = await this.recipesRepository.GetAllRecipes();
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
        public async Task<ActionResult<RecipeModel>> GetRecipe([FromRoute] string id)
        {
            var foundRecipe = await this.recipesRepository
                .GetRecipeById(id);

            if (foundRecipe == null)
            {
                return NotFound();
            }

            return mapper.Map<RecipeModel>(foundRecipe);
        }

        ///<summary>
        ///Filter all recipes with the specified ingredients
        ///</summary>
        ///<param name="ingredients"></param>
        ///<returns></returns>
        [HttpGet("search")]
        //add time and category
        public async Task<ActionResult<RecipeModel[]>> GetRecipeByIngredient([FromQuery] string[] ingredients)
        {
            var foundRecipes = await this.recipesRepository
                .GetRecipeByIngredients(ingredients);

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
            
            this.mapper.Map(updatedRecipe, existingRecipe.Result);

            this.recipesRepository.UpdateRecipe(existingRecipe.Result);

            return this.mapper.Map<RecipeModel>(existingRecipe.Result);
        }

        /// <summary>
        /// Deletes a recipe with the given id
        /// </summary>
        /// <param name="id">
        /// Id of the recipe to be deleted
        /// </param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Recipe with the specified id not found</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(string id)
        {
            var recipeToBeDeleted = this.recipesRepository.GetRecipeById(id);
            if (recipeToBeDeleted.Result == null)
            {
                return NotFound($"Could not find recipe with id = {id}");
            }

            this.recipesRepository.DeleteRecipe(id);

            return Ok();
        }

        /// <summary>
        /// Get all recipes that take at most the given amount of minutes
        /// </summary>
        /// <param name="maxTime">
        /// Maximum amount of time a recipe can take
        /// </param>
        /// <returns></returns>
        [HttpGet("time/{maxTime}")]
        public async Task<ActionResult<RecipeModel[]>> GetRecipesByTime(double maxTime)
        {
            var recipesByTime = await this.recipesRepository.GetRecipesByTime(maxTime);

            return this.mapper.Map<RecipeModel[]>(recipesByTime);
        }

        /// <summary>
        /// Get all recipes in a certain category
        /// </summary>
        /// <param name="category">
        /// Name of the category of the recipe
        /// </param>
        /// <returns></returns>
        [HttpGet("category/{category}")]
        public async Task<ActionResult<RecipeModel[]>> GetRecipesByCategory(string category)
        {
            var recipesByTime = await this.recipesRepository.GetRecipesByCategory(category);

            return this.mapper.Map<RecipeModel[]>(recipesByTime);
        }
    }
}