using Microsoft.AspNetCore.Mvc;

namespace Recipes.Controllers
{
    using Constants;
    using Domain;
    using Models;

    [Route(MainRoutes.UsersRoute)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ITokenProvider tokenProvider;

        public UsersController(ITokenProvider tokenProvider)
        {
            this.tokenProvider = tokenProvider;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> GetTokenForUser(UserModel user)
        {
            var token = this.tokenProvider.GenerateToken(user.Username);

            return Ok(token);
        }

        [HttpPost]
        public ActionResult<UserModel> AddUser(UserModel newUser)
        {
            return Ok(new UserModel { Username = newUser.Username, Password = newUser.Password });
        }
    }
}
