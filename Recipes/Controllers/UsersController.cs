using Microsoft.AspNetCore.Mvc;

namespace Recipes.Controllers
{
    using System;
    using Constants;
    using Models;

    [Route(MainRoutes.UsersRoute)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost("authenticate")]
        public ActionResult<string> GetTokenForUser(UserModel user)
        {
            var token = Guid.NewGuid();
            return Ok(token);
        }

        [HttpPost]
        public ActionResult<UserModel> AddUser(UserModel newUser)
        {
            return Ok(new UserModel { Username = newUser.Username, Password = newUser.Password });
        }
    }
}
