namespace Recipes.Persistence
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Domain;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class JwtTokenProvider : ITokenProvider
    {
        private readonly IOptions<JwtSettings> jwtSettings;

        public JwtTokenProvider(IOptions<JwtSettings> jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        public string GenerateToken(string username)
        {
            var secret = this.jwtSettings.Value.BaseSecret;
            var key = Convert.FromBase64String(secret);
            var securityKey = new SymmetricSecurityKey(key);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(System.Security.Claims.ClaimTypes.Name, username),
                    new Claim(ClaimTypes.IsAdmin, "")
                }),
                Expires = DateTime.UtcNow.AddMinutes(this.jwtSettings.Value.ExpiryTimeInMinutes),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                Issuer = this.jwtSettings.Value.Issuer
                
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.CreateJwtSecurityToken(descriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
