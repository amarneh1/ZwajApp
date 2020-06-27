using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZwajApp.API.Data;
using ZwajApp.API.Dtos;
using ZwajApp.API.Models;

namespace ZwajApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository _repo ;
        private readonly IConfiguration _config ;
        public AuthController(IAuthRepository repo,IConfiguration config)
        {
            _config=config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register ([FromBody]UserForRegisterDto userForRegisterDto){
            //No Need NextLine ModelState Validation If We use Attribute [ApiController] Also No Need
            // To Use [FromBody] Attribute If We use Attribute [ApiController]
            if(!ModelState.IsValid) return BadRequest(ModelState);
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
             if (await _repo.UserExists(userForRegisterDto.Username))
             return BadRequest("هذا المستخدم مسجل مسبقاً في الموقع");
             var UserToCreate = new User{
                 UserName = userForRegisterDto.Username
             };
             var CreatedUser = await _repo.Register(UserToCreate,userForRegisterDto.Password);
             return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto){
            var userFromRepo = await _repo.Login(userForLoginDto.username.ToLower(),userForLoginDto.password);
            if(userFromRepo==null) return Unauthorized();
            var claims = new []{
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.UserName)
            };
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(Key,SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new{
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}