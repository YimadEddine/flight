using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OAuth_Project.Interfaces;
using OAuth_Project.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OAuth_Project.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AuthenticationService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IConfiguration configuration)
        {

            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;

        }
        public async Task<string> AuthenticateAsync(string username, string password)
        { return ""; }
        //public async Task<string> AuthenticateAsync(string username, string password)
        //{
        //    AppUser user = await _userManager.FindByEmailAsync(username);
        //    if (user == null)
        //    {
        //        return "User not found";
        //    }
        //    try
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
        //        if (!result.Succeeded)
        //        {
        //            return "invalid password";
        //        }
        //        else
        //        {
        //            return GenerateJwtToken(user);
        //        }
        //    }
        //    catch (Exception ex) { Debug.WriteLine("login catch", ex); return "error"; }

        //}
        public async Task<TokenUserModel> AuthenticateAsync2(string username, string password)
        {
            AppUser user = await _userManager.FindByEmailAsync(username);
            TokenUserModel model = new TokenUserModel();
            if (user == null)
            {
                model.User = null;
                model.Token = "User not found";
                return model;
            }
            try
            {
                var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    model.Token = "invalid password";
                    model.User = null;
                    return model;
                }
                else
                {
                    model.Token=GenerateJwtToken(user);
                    model.User = user;
                    return model;
                }
            }
            catch (Exception ex){
                Debug.WriteLine("login catch", ex);
                model.Token = "error";
                model.User = null;
                return model;
            }

        }

        //public string GenerateJwtToken2(AppUser user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var authClaims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name,user.Email),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };
        //    var authKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
        //    var token = new JwtSecurityToken(
        //        issuer: _configuration["JWT:ValidIssuer"],
        //        audience: _configuration["JWT:ValidAudience"],
        //        claims: authClaims,
        //        signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha512Signature));


        //    return tokenHandler.WriteToken(token);

        //}
        public string GenerateJwtToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var authClaims = new List<Claim>
    {
      
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            
            var secretKey = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);

           
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(secretKey), 
                SecurityAlgorithms.HmacSha512Signature 
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"], 
                audience: _configuration["JWT:ValidAudience"], 
                claims: authClaims, 
                expires: DateTime.UtcNow.AddHours(500), 
                signingCredentials: signingCredentials 
            );

           
            return tokenHandler.WriteToken(token);
        }

    }
}
