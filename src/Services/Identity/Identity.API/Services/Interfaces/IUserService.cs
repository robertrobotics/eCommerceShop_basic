using System.Collections.Generic;
using Identity.API.Entities;
using Identity.API.Models;

namespace Identity.API.Services.Interfaces
{
    public interface IUserService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}