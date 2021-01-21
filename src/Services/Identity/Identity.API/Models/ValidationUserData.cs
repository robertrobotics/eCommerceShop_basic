using Identity.API.Interfaces;

namespace Identity.API.Models
{
    public class ValidationUserData : IValidationUserData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}