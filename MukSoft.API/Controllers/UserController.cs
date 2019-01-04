using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MukSoft.Core.Domain;
using MukSoft.Services.User.Command;
using MukSoft.Services.User.Query;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MukSoft.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IConfiguration _config;
        public UserController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;
        }


        #region "Get User"       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new ReadUserQuery());
            return result != null ? (IActionResult)Ok(result) : StatusCode(500);
        }
        #endregion

        #region "Insert User"
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest(ModelState);
            var id = await _mediator.Send(new InsertUserCommand(model));
            return string.IsNullOrEmpty(id.ToString()) ? NotFound() : (IActionResult)Created(string.Empty, id);
        }

        #endregion

        #region "Validate And Generate JWT For User"
        [AllowAnonymous]
        [HttpGet("{email}/{password}", Name = "GetUser")]
        public async Task<IActionResult> GetUserByCondition([FromRoute] string email, [FromRoute]string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Cannot create token");
            }
            var result = await _mediator.Send(new ReadByConditionUserQuery(email, password));
            if (result == null)
            {
                return Unauthorized();
            }
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Sub,  "suject"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //shared key between the token server and the resource server
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("condimentumvestibulumSuspendissesitametpulvinarorcicondimentummollisjusto"));
            var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var SecurityToken = new JwtSecurityToken(
                issuer: _config["AuthSection:JWtConfig:Issuer"],
                audience: _config["AuthSection:JWtConfig:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(SecurityToken),
                expiration = SecurityToken.ValidTo
            });

        }

        #endregion

    }
}