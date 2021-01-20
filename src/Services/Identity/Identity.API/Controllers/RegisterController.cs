using System;
using System.Threading.Tasks;
using Identity.API.Data;
using Identity.API.Helpers;
using Identity.API.Interfaces;
using Identity.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher _passwordHasher;
        private IEncryptedUser _encryptedUser;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher();    
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationInfo userRegistrationInfo)
        {
            if (string.IsNullOrEmpty(userRegistrationInfo.Username) ||
                string.IsNullOrEmpty(userRegistrationInfo.FirstName) ||
                string.IsNullOrEmpty(userRegistrationInfo.LastName) ||
                string.IsNullOrEmpty(userRegistrationInfo.CityName))
            {
                return BadRequest("Could not register user - wrong user's data.");
            }

            _encryptedUser = _passwordHasher.GetEncryptedUserInfo(userRegistrationInfo.Password);

            var newUser = new User()
            {
                Username = userRegistrationInfo.Username,
                FirstName = userRegistrationInfo.FirstName,
                LastName = userRegistrationInfo.LastName,
                HashedPassword = _encryptedUser.HashedPassword,
                CityName = userRegistrationInfo.CityName
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}