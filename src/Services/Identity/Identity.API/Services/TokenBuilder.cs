using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Services
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly byte[] _symmetricKey = Encoding.UTF8.GetBytes("sqvMKS8Sg8UtYWQDj6jtjPJOerh0KrAcm0dkKAgvuDk");
        public string BuildToken(string username)
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(_symmetricKey), SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username)
            };
            var jwtSecurityToken = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}