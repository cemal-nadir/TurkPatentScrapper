//using IdentityModel;
//using IdentityServer4.Validation;
//using Microsoft.AspNetCore.Identity;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using TPHunter.WebServices.Identity.API.Models;

//namespace TPHunter.WebServices.Identity.API.Services
//{
//    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
//        {
//            _userManager = userManager;
//        }
//        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
//        {
//            var errors = new Dictionary<string, object>();
//            var existUser = await _userManager.FindByEmailAsync(context.UserName);
//            if (existUser is null)
//            {
//                errors.Add("errors", new List<string> { "Email veya Şifreniz Yanlış" });
//                context.Result.CustomResponse = errors;
//                return;
//            }
//            var passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);
//            if (!passwordCheck)
//            {
//                errors.Add("errors", new List<string> { "Email veya Şifreniz Yanlış" });
//                context.Result.CustomResponse = errors;
//                return;
//            }
//            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);

//        }
//    }
//}
