using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OAuth_Project.Data;
using OAuth_Project.Interfaces;
using OAuth_Project.Model_Binders;
using OAuth_Project.Models;
using OAuth_Project.Services;
using System.Diagnostics;

namespace OAuth_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AccountService _accountService;
        private readonly IAuthenticationService _authenticationService;
        public AccountController(UserManager<AppUser> userManager, IAccountRepository accountRepository, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _accountService = new AccountService(accountRepository);
            _authenticationService = authenticationService;

        }

        [HttpPost]
        public async Task<IActionResult> registerAccount([ModelBinder(BinderType = typeof(AppUserModelBinder))] AppUser appUser)
        {
           
            

            if(appUser == null)  return BadRequest("Couldn't create the account");
            var res = await _accountService.createUserAccountIndentity(appUser);
            return  Ok( new{ result = res });
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginModel user)
        {
            var result = await _authenticationService.AuthenticateAsync2(user.Email, user.Password);
            if(result.Token == "error") return Unauthorized(result);

            return Ok(new {token=result.Token, user = result.User});
        }
        [Authorize]
        [HttpGet("test")]
        public IActionResult apitest()
        {
            var response="";
            if (User.Identity.IsAuthenticated) { return Ok(); }




            return BadRequest();
        }
      
    }
}
