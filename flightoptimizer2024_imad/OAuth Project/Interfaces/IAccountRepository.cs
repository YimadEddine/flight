using Microsoft.AspNetCore.Identity;
using OAuth_Project.Models;

namespace OAuth_Project.Interfaces
{
    public interface IAccountRepository
    {
        Task<AppUser> insertUserAccount(AppUser user);
        Task<string> insertUserAccountIdentityResult(AppUser user);
        AppUser getUserAccount(AppUser user);
     
    }
}
