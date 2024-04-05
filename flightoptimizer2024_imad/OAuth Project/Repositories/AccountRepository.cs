using Microsoft.AspNetCore.Identity;
using OAuth_Project.Data;
using OAuth_Project.Interfaces;
using OAuth_Project.Models;
using System.Diagnostics;

namespace OAuth_Project.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Data.MyAppContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        

        public AccountRepository(Data.MyAppContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<AppUser> insertUserAccount(AppUser userToInsert)
        {
            Debug.WriteLine("user to tinsert obj : ", userToInsert );
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
          
            }
            var pwd = userToInsert.PasswordHash;
            userToInsert.PasswordHash = null;
            var user = await _userManager.FindByEmailAsync(userToInsert.Email);
            if (user == null)
            {             
                var result = await _userManager.CreateAsync(userToInsert, pwd);
                var test = 0;
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userToInsert, "User");
                    return userToInsert;
                }
                else
                {
                   
                    return null;
                }

                
            }
            return  null;
        }
        public async Task<string> insertUserAccountIdentityResult(AppUser userToInsert)
        {
            Debug.WriteLine("user to tinsert obj : ", userToInsert);
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));

            }
            var pwd = userToInsert.PasswordHash;
            userToInsert.PasswordHash = null;
            var user = await _userManager.FindByEmailAsync(userToInsert.Email);
            int test = 0;
            if (user == null)
            {
                var result = await _userManager.CreateAsync(userToInsert, pwd);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userToInsert, "User");
                    return result.ToString();

                }
                
            }
            return "error";
        }

        public AppUser getUserAccount(AppUser user)
        {
            throw new NotImplementedException();
        }

       
    }
}
