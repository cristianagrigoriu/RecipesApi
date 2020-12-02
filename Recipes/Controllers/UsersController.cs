using Microsoft.AspNetCore.Mvc;

namespace Recipes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using Constants;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.IdentityModel.Tokens;
    using Models;

    [Route(MainRoutes.UsersRoute)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost("authenticate")]
        public ActionResult<string> GetTokenForUser(UserModel user)
        {
            var token = this.GenerateToken(user.Username);

            return Ok(token);
        }
        
        private string GenerateToken(string username)
        {
            var secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";
            var key = Convert.FromBase64String(secret);
            var securityKey = new SymmetricSecurityKey(key);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, username)}),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.CreateJwtSecurityToken(descriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        [HttpPost]
        public ActionResult<UserModel> AddUser(UserModel newUser)
        {
            return Ok(new UserModel { Username = newUser.Username, Password = newUser.Password });
        }
    }
}
