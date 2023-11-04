using HavaDurumu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HavaDurumu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthIdentityController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AuthIdentityController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] ApiUser apiUserInformation)
        {
            var apiUser = AuthenticateUser(apiUserInformation);
            if (apiUser == null) return NotFound("User not found");

            var token = CreateToken(apiUser);

            return Ok(token);
        }

        private string CreateToken(ApiUser apiUser)
        {
            if (_jwtSettings.Key == null) throw new Exception("Jwt ayarlarındaki Key değeri null olamaz.");
           

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimArray = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, apiUser.UserName!),
                new Claim(ClaimTypes.Role, apiUser.Role!)
            };

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claimArray,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ApiUser? AuthenticateUser(ApiUser apiUserInformation)
        {
            return ApiUsersDto
                .ApiUsers
                .FirstOrDefault(x =>
                x.UserName == apiUserInformation.UserName 
                && x.Password == apiUserInformation.Password
            );
        }
    }
}
