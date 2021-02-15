using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Recipes.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Constants;
    using Domain;
    using Microsoft.AspNetCore.Authorization;
    using Models;

    [Route(MainRoutes.UsersRoute)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ITokenProvider tokenProvider;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UsersController(ITokenProvider tokenProvider,
            IUserRepository userRepository,
            IMapper mapper)
        {
            this.tokenProvider = tokenProvider;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> GetTokenForUser(LoginUserModel user)
        {
            //ToDo (criptat parole dupa)
            //ToDo diferenta criptat vs hashing pentru parole

            var userFromDatabase = await this.userRepository.GetUserByUsername(user.Username);

            if (userFromDatabase == null)
            {
                return NotFound();
            }

            if (userFromDatabase.Password == user.Password)
            {
                var token = this.tokenProvider.GenerateToken(user.Username);
                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> AddUser(LoginUserModel newUser)
        {
            var isUserNameTaken = (await this.userRepository.GetUserByUsername(newUser.Username)) != null;

            if (isUserNameTaken)
            {
                return Conflict("Username is already taken");
            }

            var userToAdd = this.mapper.Map<User>(newUser);
            this.userRepository.AddUser(userToAdd);
            return Created("", this.mapper.Map<UserModel>(userToAdd));
        }

        [HttpPost("favourites")]
        [Authorize]
        [Authorize(Policy = "ShouldBeAdmin")]
        public async Task<ActionResult<UserModel>> SetRecipeAsFavourite(string recipeId)
        {
            var userName = this.HttpContext.User.Identity.Name;
            var userToUpdate = await this.userRepository.GetUserByUsername(userName); //ToDo separate database for claims?

            var isRecipeAlreadyFavourite = userToUpdate.FavouriteRecipes.Any(x => x == recipeId);

            if (isRecipeAlreadyFavourite)
            {
                return Ok(this.mapper.Map<UserModel>(userToUpdate)); //ToDo should return something else when recipe already there??
            }

            userToUpdate.FavouriteRecipes = userToUpdate.FavouriteRecipes.Append(recipeId); //ToDo check if recipe already there

            var updatedUser = await this.userRepository.UpdateUser(userToUpdate);
            return Ok(this.mapper.Map<UserModel>(updatedUser));
            //ToDo investigate claims in authorization
        }

        //ToDo add comment to recipe
    }
}
