using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        private readonly IHashGenerator hashGenerator;
        private readonly ILogger<UsersController> logger;

        public UsersController(ITokenProvider tokenProvider,
            IUserRepository userRepository,
            IMapper mapper,
            IHashGenerator hashGenerator,
            ILogger<UsersController> logger)
        {
            this.tokenProvider = tokenProvider;
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.hashGenerator = hashGenerator;
            this.logger = logger;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> GetTokenForUser(LoginUserModel user)
        {
            var userFromDatabase = await this.userRepository.GetUserByUsername(user.Username);

            if (userFromDatabase == null)
            {
                return NotFound();
            }

            if (this.PasswordIsCorrect(user.Password, userFromDatabase.Password))
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

            newUser.Password = this.hashGenerator.GenerateHashFor(newUser.Password);

            var userToAdd = this.mapper.Map<User>(newUser);
            this.userRepository.AddUser(userToAdd);
            return Created("", this.mapper.Map<UserModel>(userToAdd));
        }

        [HttpPost("favourites")]
        [Authorize]
        [Authorize(Policy = "ShouldBeAdmin")]
        public async Task<ActionResult<UserModel>> SetRecipeAsFavourite(string recipeId)
        {
            var user = User.Identity.Name;
            var claims = User.Claims;
            this.logger.LogInformation("{user} will mark recipe {recipeId} as favourite because they have the claims: {claims}", 
                user, recipeId, claims);

            this.logger.LogCritical("We have the claims: {claims}", claims);

            var userName = this.HttpContext.User.Identity.Name;
            var userToUpdate = await this.userRepository.GetUserByUsername(userName); //ToDo separate database for claims?

            var isRecipeAlreadyFavourite = userToUpdate.FavouriteRecipes.Any(x => x == recipeId);

            if (isRecipeAlreadyFavourite)
            {
                return Ok(this.mapper.Map<UserModel>(userToUpdate));
            }

            userToUpdate.FavouriteRecipes = userToUpdate.FavouriteRecipes.Append(recipeId);

            var updatedUser = await this.userRepository.UpdateUser(userToUpdate);
            return Ok(this.mapper.Map<UserModel>(updatedUser));
            //ToDo investigate claims in authorization
        }

        private bool PasswordIsCorrect(string inputPassword, string passwordFromDatabase)
        {
            var inputPasswordHash = this.hashGenerator.GenerateHashFor(inputPassword);
            return inputPasswordHash == passwordFromDatabase;
        }

        //ToDo add comment to recipe
    }
}
