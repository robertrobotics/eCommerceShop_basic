using System;
using System.Threading.Tasks;
using Identity.API.Data;
using Identity.API.Helpers;
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
        public async Task<IActionResult> Login([FromBody]ValidationUserData user)
        {
            var dbUser = await _context.Users.SingleOrDefaultAsync(_user => _user.Username.Equals(user.Username));
            if (dbUser == null)
                return NotFound("User does not exist.");

            var validationResult = await ValidateUser(user);

            return validationResult ? Ok(/*token*/) : BadRequest("Wrong username or password."); 
        }

        private async Task<bool> ValidateUser(ValidationUserData userToBeValidated)
        {
            bool userValidated = false;
            var userDb = await _context.Users.FirstAsync(u => userToBeValidated.Username == u.Username);
            if (userDb == null)
                return false;
            
            var passwordHasher = new PasswordHasher();
            byte[] hashedPasswordBytes = Convert.FromBase64String(userDb.HashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashedPasswordBytes, salt, 16);

            var userValidationHashedPassword = passwordHasher.GetEncryptedUserInfo(userToBeValidated.Password);

            if (userValidationHashedPassword.HashedPassword == userDb.HashedPassword &&
                userToBeValidated.Username == userDb.Username)
            {
                // User validated sucessfully
                userValidated = true;
            }
            
            return userValidated;
        }
    }
}