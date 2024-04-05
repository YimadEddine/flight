using Microsoft.AspNetCore.Identity;
using OAuth_Project.Interfaces;
using OAuth_Project.Models;
using System.Diagnostics;

namespace OAuth_Project.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository) {
            _accountRepository = accountRepository;
        }

        public async Task<AppUser> createUserAccount(AppUser user)
        {
       
            return await _accountRepository.insertUserAccount(user);
        }
        public async Task<string> createUserAccountIndentity(AppUser user)
        {

            return await _accountRepository.insertUserAccountIdentityResult(user);
        }
    }
}
