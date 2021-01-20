namespace Identity.API.Interfaces
{
    public interface IEncryptedUser 
    {
        string Password { get; set; }
        string HashedPassword { get; set; }
    }
}