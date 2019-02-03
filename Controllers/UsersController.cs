using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WorkDayLog.Core.Configurations;
using WorkDayLog.Domain.Users;
using WorkDayLog.Domain.Users.Services;
using WorkDayLog.Requests;

namespace WorkDayLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
            => _userService = userService;

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register([FromBody] RegisterUser register)
        {
            var user = Domain.Users.User.New(register.Name, register.Email, register.Password);
            var result = _userService.Save(user);

            return result ?
                StatusCode(StatusCodes.Status201Created) :
                BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public ActionResult<object> Login([FromBody] Login login,
                                  [FromServices]SigningConfigurations signingConfigurations,
                                  [FromServices]TokenConfigurations tokenConfigurations)
        {
            var user = _userService.Authenticate(login.Email, login.Password);

            if (user == null) return Unauthorized();

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Name, "Login"),
                new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email?.Address)
                }
            );

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddSeconds(tokenConfigurations.Seconds)
            });

            return new
            {
                authenticated = true,
                accessToken = handler.WriteToken(securityToken),
                message = "OK"
            };
        }
    }
}