//using Microsoft.AspNetCore.Identity;
//using System.Linq;
//using System.Threading.Tasks;
//using TPHunter.WebServices.Identity.API.Dtos;
//using TPHunter.WebServices.Identity.API.Models;

//namespace TPHunter.WebServices.Identity.API.Helpers
//{
//    public class UserEditHelper
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        public UserEditHelper(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//        }
//        public async Task UndoChanges(UpdateUserDto userCache)
//        {
//            var updatedUser =await _userManager.FindByIdAsync(userCache.ID);
//            updatedUser.City = userCache.City;
//            updatedUser.Email = userCache.Email;
//            updatedUser.UserName = userCache.UserName;

//            updatedUser.Address = userCache.Address;
//            updatedUser.Country = userCache.Country;
//            updatedUser.County = userCache.County;
//            updatedUser.Gender = userCache.Gender;
//            updatedUser.MainLanguage = userCache.MainLanguage;
//            updatedUser.Name = userCache.Name;
//            updatedUser.PhoneNumber = userCache.PhoneNumber;
//            updatedUser.PostalCode = userCache.PostalCode;
//            updatedUser.SurName = userCache.SurName;

//            await _userManager.UpdateAsync(updatedUser);
//            var allRoles = _roleManager.Roles.ToList();
//            foreach (var role in allRoles)
//            {
//                await _userManager.RemoveFromRoleAsync(updatedUser, role.Name);
//            }
//           await _userManager.AddToRoleAsync(updatedUser, userCache.RoleName);
//        }
//    }
//}
