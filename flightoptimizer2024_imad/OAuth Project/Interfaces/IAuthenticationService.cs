using OAuth_Project.Models;

namespace OAuth_Project.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task<TokenUserModel> AuthenticateAsync2(string username, string password);
        string GenerateJwtToken(AppUser user);
    }
}
