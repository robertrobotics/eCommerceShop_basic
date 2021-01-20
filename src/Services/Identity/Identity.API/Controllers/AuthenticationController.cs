using System.Threading.Tasks;
using Identity.API.Data;
using Identity.API.Models;
using Identity.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenBuilder _tokenBuilder;

        public AuthenticationController(ApplicationDbContext context, ITokenBuilder tokenBuilder)
        {
            _context = context;
            _tokenBuilder = tokenBuilder;
        } 

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]User user)
        {
            var dbUser = await _context.Users.SingleOrDefaultAsync(_user => _user.Username.Equals(user.Username));

            if (dbUser == null)
                return NotFound("User does not exist.");

            var validationResult = await ValidateUser(user);

            return validationResult ? Ok(/*token*/) : BadRequest("Wrong username or password."); 
        }

        private async Task<bool> ValidateUser(User userToBeValidated)
        {
            var userDb = await _context.Users.FirstAsync(u => userToBeValidated.Username == u.Username);
            if (userDb == null)
                return false;
            
            // TODO: check hashed password with candidate's info
            return false;
        }
    }
}