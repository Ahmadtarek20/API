using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AppApi.Data;
using AppApi.Dto;
using AppApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AppApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthRepository _rebo;
        private readonly IConfiguration _confg;
        public AuthController (IAuthRepository rebo, IConfiguration confg) {
            _confg = confg;
            _rebo = rebo;

        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register (UserForRigesterDto userForRigesterDto)
         {
            //validathan
            userForRigesterDto.Username = userForRigesterDto.Username.ToLower ();
            if (await _rebo.UserExists (userForRigesterDto.Username))
                return BadRequest ("this is exest");
            var userstocreat = new Users {
                UserName = userForRigesterDto.Username
            };
            var CreatedUser = await _rebo.Register (userstocreat, userForRigesterDto.Password);
            return StatusCode (201);
        }

        [HttpPost ("login")]
        public async Task<IActionResult> Login (UserForLoginDto userForLogin)
        {
            var userfromrebo = await _rebo.Login (userForLogin.username.ToLower (), userForLogin.password);
            if (userfromrebo == null) return Unauthorized ();
            var clamis = new [] {
                new Claim(ClaimTypes.NameIdentifier, userfromrebo.id.ToString ()),
                new Claim(ClaimTypes.Name, userfromrebo.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confg.GetSection("AppSettings:Token").Value));
            var cards = new SigningCredentials(key,SecurityAlgorithms.HmacSha512);
            var tokenDesscrptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(clamis),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials=cards
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesscrptor);
            return Ok(new {
                token= tokenHandler.WriteToken(token)
            });

        }

    }
}