using Identity.API.Interfaces;

namespace Identity.API.Models
{
    public class EncryptedUser : IEncryptedUser
    {
        public string Password { get; set; }
        public string HashedPassword { get; set; }
    }
}