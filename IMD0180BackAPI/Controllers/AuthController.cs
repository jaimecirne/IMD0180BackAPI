using AutoMapper;
using IMD0180BackAPI.Data;
using IMD0180BackAPI.DTO;
using IMD0180BackAPI.Model;
using IMD0180BackAPI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Security.Principal;
using IMD0180BackAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace IMD0180BackAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly UserContex _context;

        private readonly ICriptoServices _criptoServices;

        private readonly SigningConfigurations _signingConfigurations;
        
        private readonly TokenConfiguration _tokenConfigurations;

        public AuthController(ILogger<UserController> logger,
            UserContex context,
            ICriptoServices criptoServices,
            SigningConfigurations signingConfigurations,
            TokenConfiguration tokenConfigurations)
        {
            _context = context;
            _logger = logger;
            _criptoServices = criptoServices;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            
        }

        [HttpPost]
        public IActionResult Login([FromBody] CreateUserDTO userDTO)
        {
            User user = _context.Users.Where(user => user.Login == userDTO.Login).FirstOrDefault();

            if (user == null) 
            {
                return NotFound();
            }

            string password = _criptoServices.Hash(userDTO.Login, userDTO.Password);

            if (user.Password != password)
            {
                return NotFound();
            }

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Login, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Login),
                        new Claim(ClaimTypes.Role, user.Role)
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            return Ok(new
            {
                authenticated = true,
                created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            });
        }
    }
}
