using ChatApplication.Models;

namespace ChatApplication.AuthService
{
    public interface IAuthService
    {
        AuthResult AuthenticateWithAdoNet(string email, string password);
    }
}