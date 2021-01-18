using Identity.API.Data;
using Identity.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;    
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]User user)
        {
            
        }
    }
}