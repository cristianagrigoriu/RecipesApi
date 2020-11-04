namespace Recipes.Controllers
{
    using System.Collections.Generic;
    using Constants;
    using Data;
    using Microsoft.AspNetCore.Mvc;

    [Route(MainRoutes.InstructionsRoute)]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IRecipesRepository recipesRepository;

        public IngredientsController(IRecipesRepository recipesRepository)
        {
            this.recipesRepository = recipesRepository;
        }

        [HttpGet]
        public ActionResult<List<string>> Get(string id)
        {
            var recipe = this.recipesRepository.GetRecipeById(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var instructions = this.recipesRepository.GetInstructionsOfRecipe(id);

            return instructions;
        }
    }
}
