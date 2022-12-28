using AutoMapper;
using IMD0180BackAPI.Data;
using IMD0180BackAPI.DTO;
using IMD0180BackAPI.Model;
using IMD0180BackAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using IMD0180BackAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace IMD0180BackAPI.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly UserContex _context;

        private readonly IMapper _mapper;

        private readonly ICriptoServices _criptoServices;

        private readonly SigningConfigurations _signingConfigurations;
        
        private readonly TokenConfiguration _tokenConfigurations;

        public UserController(ILogger<UserController> logger,
            UserContex context,
            IMapper mapper,
            ICriptoServices criptoServices,
            SigningConfigurations signingConfigurations,
            TokenConfiguration tokenConfigurations)
        {
            _context = context;
            _mapper = mapper;            
            _logger = logger;
            _criptoServices = criptoServices;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUser([FromBody] CreateUserDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            user.Password = _criptoServices.Hash(user.Login, user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { Id = user.Id }, user);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateUser(int id, [FromForm] UpdateUserDTO userDTO)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user = _mapper.Map(userDTO, user);
            _context.Update(user);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        [Authorize]
        public IActionResult All ()
        {
            return Ok(_context.Users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return Ok(_mapper.Map<ShowUserDTO>(user));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
