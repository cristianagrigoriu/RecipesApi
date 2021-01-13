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
        public async Task<ActionResult<string>> GetTokenForUser(UserModel user)
        {
            //ToDo (criptat parole dupa)
            //ToDo diferenta criptat vs hashing pentru parole

            //var users = await this.userRepository.GetAllUsers();
            //var userModels = this.mapper.Map<UserModel[]>(users);

            var userFromDatabase = await this.userRepository.GetuserByUsername(user.Username);

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
        public ActionResult<UserModel> AddUser(UserModel newUser)
        {
            return Ok(new UserModel { Username = newUser.Username, Password = newUser.Password });
        }

        [HttpPost("favourites")]
        [Authorize]
        public void SetRecipeAsFavourite(string recipeId)
        {
            //ToDo investigate claims in authorization
        }
    }
}
