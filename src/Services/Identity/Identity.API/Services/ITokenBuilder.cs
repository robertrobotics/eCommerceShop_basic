namespace Identity.API.Services
{
    public interface ITokenBuilder
    {
        string BuildToken(string username);
    }
}