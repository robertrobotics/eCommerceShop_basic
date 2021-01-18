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

            // TODO: verify user's password with hash'n'salt 
        }
    }
}