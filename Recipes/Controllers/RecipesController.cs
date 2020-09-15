using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Recipes.Data;
using Recipes.Models;
using System.Linq;

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

        [HttpGet]
        public ActionResult<RecipeModel[]> GetAllRecipes()
        {
            var foundRecipes = RecipesFactory.GetRecipesWithBasicDetails();
            var recipeModels = mapper.Map<RecipeModel[]>(foundRecipes);
            return recipeModels;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RecipeModel), 200)]
        [ProducesErrorResponseType(typeof(RecipeModel))]
        public ActionResult<RecipeModel> GetRecipe(string id)
        {
            try
            {
                var foundRecipe = RecipesFactory
                    .GetRecipesWithBasicDetails()
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if(foundRecipe == null)
                {
                    return NotFound();
                }

                return mapper.Map<RecipeModel>(foundRecipe);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}