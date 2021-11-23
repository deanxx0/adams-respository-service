using adams_repository_service.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace adams_repository_service.Controllers
{
    [ApiController]
    [Route("")]
    public class LoginController : ControllerBase
    {
        private readonly string _jwtSecretKey;
        private readonly AppDbContext _appDbContext;

        public LoginController(AppDbContext appDbContext, IConfiguration configuration)
        {
            _jwtSecretKey = configuration.GetValue<string>("JwtSecretKey");
            _appDbContext = appDbContext;
        }

        [HttpPost("login/{username}/{password}")]
        public async Task<ActionResult> Login(string username, string password)
        {
            var user = _appDbContext.Users.AsQueryable().Where(x => x.UserName == username).FirstOrDefault();
            if (user == null) return Unauthorized();
            if (user.Password != password) return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(user.UserClaim, "true"),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            HttpContext.Response.Headers.Add("access_token", tokenString);
            return Ok(tokenString);
        }
    }
}
