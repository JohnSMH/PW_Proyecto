using PW_API.Models;
using PW_API.Models.Auth;
using PW_API.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proyecto2_Web_SophiaSiguere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TorneoappContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(TorneoappContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [Route("login")]
        [HttpPost]
        public async Task<UserToken?> Login(UserAuth userCreds)
        {
            var user = await _context.Users
                            .Where(u => u.Username == userCreds.User)
                            .FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            if (Encryption.ComparePasswords(user.Password, userCreds.Password))
            {
                return new UserToken
                {
                    Id = user.Id,
                    Username = user.Username,
                    Token = CustomTokenJWT(user.Username)
                };
            }
            return null;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<UserToken>> Register(User usuarionuevo)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'MoviesdbContext.PersonalInformations'  is null.");
            }
            usuarionuevo.Password = Encryption.EncryptPassword(usuarionuevo.Password!);
            _context.Users.Add(usuarionuevo);
            await _context.SaveChangesAsync();

            return new UserToken
            {
                Id = usuarionuevo.Id,
                Username = usuarionuevo.Username,
                Token = CustomTokenJWT(usuarionuevo.Username)
            };
        }

        private string CustomTokenJWT(string username)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!)
            );
            var _signingCredentials = new SigningCredentials(
                _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
            );
            var _Header = new JwtHeader(_signingCredentials);
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, username)
            };
            var _Payload = new JwtPayload(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: _Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(2)
            );
            var _Token = new JwtSecurityToken(_Header, _Payload);
            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}
