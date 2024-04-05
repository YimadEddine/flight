using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OAuth_Project.Models;
using System.Text.Json;

namespace OAuth_Project.Model_Binders
{
    public class AppUserModelBinder : IModelBinder
    {
        private readonly UserManager<AppUser> _userManager;

        public AppUserModelBinder(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);
            var jsonBody = await reader.ReadToEndAsync();

            try
            {
               
                var signUpModel = JsonSerializer.Deserialize<SignUpModel>(jsonBody);

                if (signUpModel.Password != signUpModel.ConfirmPassword)
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    return;
                }
               // var passwordHash = _userManager.PasswordHasher.HashPassword(null, signUpModel.Password);
                var appUser = new AppUser
                {
                    FirstName = signUpModel.FirstName,
                    LastName = signUpModel.LastName,
                    Email = signUpModel.Email,
                    PhoneNumber = signUpModel.PhoneNumber,
                    TwoFactorEnabled = false,
                    UserName = signUpModel.Email,
                    PasswordHash = signUpModel.Password
                };

                bindingContext.Result = ModelBindingResult.Success(appUser);
            }
            catch (JsonException) { 
        
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }



    }






}
