using JWTPractice.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTPractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
     
        public static User user=new User();

        private IConfiguration _config;

        public AuthController(IConfiguration configuration)
        {
            _config = configuration;
        }


        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.passwordHashed);
            user.userName=request.userName;
            user.password = passwordHash;
            return user;

        }

        [HttpPost("login")]
        public ActionResult LogIn(UserDto request)
        {
            ActionResult response = Unauthorized();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.passwordHashed);
            if (BCrypt.Net.BCrypt.Verify(request.passwordHashed, user.password))
            {
                var tk = GenerateTocken(request);
                response = Ok(new { token = tk });
            }
            else if(!BCrypt.Net.BCrypt.Verify(request.passwordHashed,user.password))
            {
                response = BadRequest(new { msg = "Pasword is incorrect" });
            }
            else
            {
                response = BadRequest(new { msg = "User not Exist" });
            }
            user.userName = request.userName;
            user.password = passwordHash;
            
            return response;

        }

        [AllowAnonymous] //what does mean
        private String GenerateTocken(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var cred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //var roles = "Admin";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.userName),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Expiration, string.Join(",", DateTime.Now.AddMinutes(1))),
                // Add other claims as needed
            };
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims
                , signingCredentials: cred
                ,expires: DateTime.Now.AddMinutes(1)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpGet("DummyAuth"), Authorize(Roles = "Admin")]
        public String DummyAuth()
        {
            return "a";
        }

    }
}
