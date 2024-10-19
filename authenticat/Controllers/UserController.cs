using authenticat.Models;
using authenticat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace authenticat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       
            private readonly IUser _user;
            private readonly IConfiguration _configuration;
            public UserController(IUser user, IConfiguration config)
            {
                _user = user;
                _configuration = config;
            }

            [HttpPost("register")]
            public IActionResult register(user user)
            {
                _user.Register(user);
                return Ok();

            }
            [HttpPost("login")]
            public IActionResult Login(Login user)
            {
                try
                {
                    var u = _user.Login(user);
                    if (u == null)
                    {
                        return BadRequest("Invald username or password");
                    }
                    string token = CreateToken(u);
                    return Ok(token);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Internal server error");
                }
            }

            [Authorize]
            [HttpGet("getuser")]
            public IActionResult GetUser()
            {
                return Ok(_user.GetUsers());
            }

            private string CreateToken(user user)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim (ClaimTypes.Name,user.UserName),
                new Claim (ClaimTypes.Role, user.Role)
            };

                var token = new JwtSecurityToken(
                        claims: claims,
                        signingCredentials: credentials,
                        expires: DateTime.Now.AddDays(1)
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }




