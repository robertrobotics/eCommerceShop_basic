using Identity.API.Models;
using Identity.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase   
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;   
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticationRequest authRequest)
        {
            var authResponse = _userService.Authenticate(authRequest);

            if (authRequest == null)
                return BadRequest(new {message = "Username or password is incorrect."});
            
            return Ok(authResponse);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUsers() => Ok(_userService.GetAll());
    }
}