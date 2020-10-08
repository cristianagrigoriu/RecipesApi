namespace Recipes.Controllers
{
    using System.Collections.Generic;
    using Constants;
    using Data;
    using Microsoft.AspNetCore.Mvc;

    [Route(MainRoutes.InstructionsRoute)]
    [ApiController]
    public class InstructionsController : ControllerBase
    {
        private readonly IRecipesRepository recipesRepository;

        public InstructionsController(IRecipesRepository recipesRepository)
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
