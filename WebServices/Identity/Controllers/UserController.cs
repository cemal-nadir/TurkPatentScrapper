//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Threading.Tasks;
//using TPHunter.WebServices.Identity.API.Dtos;
//using TPHunter.WebServices.Identity.API.Helpers;
//using TPHunter.WebServices.Identity.API.Models;
//using TPHunter.WebServices.Identity.API.Models.Inputs;
//using TPHunter.WebServices.Shared.ApiResponse.Dtos;
//using static IdentityServer4.IdentityServerConstants;

//namespace TPHunter.WebServices.Identity.API.Controllers
//{
//    [Authorize(LocalApi.PolicyName)]
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class UserController : Controller
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly UserEditHelper _userEditHelper;
//        private readonly IMapper _mapper;
//        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
//        {
//            _userManager = userManager;
//            _mapper = mapper;
//            _roleManager = roleManager;
//            _userEditHelper = new UserEditHelper(_userManager, _roleManager);
//        }
//        //      [Authorize(Roles = "admin")]
//        [HttpPost]
//        public async Task<IActionResult> SignUp(SignupDto signupDto)
//        {
//            var user = new ApplicationUser()
//            {
//                City = signupDto.City,
//                UserName = signupDto.UserName,
//                Email = signupDto.Email,
//                Address = signupDto.Address,
//                Country = signupDto.Country,
//                County = signupDto.County,
//                Gender = signupDto.Gender,
//                Name = signupDto.Name,
//                PhoneNumber = signupDto.PhoneNumber,
//                SurName = signupDto.SurName,
//                MainLanguage = signupDto.MainLanguage
//            };

//            var result = await _userManager.CreateAsync(user, signupDto.Password);
//            if (!result.Succeeded)
//                return BadRequest(Response<string>.Fail(400, result.Errors.Select(x => x.Description).ToList()));

//            var newRole = await _roleManager.FindByNameAsync(signupDto.RoleName);

//            result = await _userManager.AddToRoleAsync(user, newRole.Name);
//            if (!result.Succeeded)
//            {
//                await _userManager.DeleteAsync(user);
//                return BadRequest(Response<string>.Fail(400, result.Errors.Select(x => x.Description).ToList()));
//            }
//            return Ok(Response<string>.Success(200, user.Id));
//        }
//        [HttpPut]
//        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
//        {
//            var userCache = await _userManager.FindByIdAsync(updateUserDto.ID);
//            var roleCache = (await _userManager.GetRolesAsync(userCache)).FirstOrDefault();
//            var userCacheDto = new UpdateUserDto()
//            {
//                City = userCache.City,
//                UserName = userCache.UserName,
//                Email = userCache.Email,
//                ID = userCache.Id,
//                RoleName = roleCache,
//                Address = userCache.Address,
//                Country = userCache.Country,
//                County = userCache.County,
//                Gender = userCache.Gender,
//                MainLanguage = userCache.MainLanguage,
//                Name = userCache.Name,
//                PhoneNumber = userCache.PhoneNumber,
//                PostalCode = userCache.PostalCode,
//                SurName = userCache.SurName
//            };
//            var user = userCache;
//            user.Id = updateUserDto.ID;
//            user.City = updateUserDto.City;
//            user.Email = updateUserDto.Email;
//            user.UserName = updateUserDto.UserName;
//            user.Address = updateUserDto.Address;
//            user.Country = updateUserDto.Country;
//            user.County = updateUserDto.County;
//            user.Gender = updateUserDto.Gender;
//            user.MainLanguage = updateUserDto.MainLanguage;
//            user.Name = updateUserDto.Name;
//            user.PhoneNumber = updateUserDto.PhoneNumber;
//            user.PostalCode = updateUserDto.PostalCode;
//            user.SurName = updateUserDto.SurName;

//            var response = await _userManager.UpdateAsync(user);


//            if (!response.Succeeded)
//            {
//                await _userEditHelper.UndoChanges(userCacheDto);
//                return BadRequest(Response<NoContent>.Fail(400, response.Errors.Select(x => x.Description).ToList()));
//            }



//            user = await _userManager.FindByIdAsync(updateUserDto.ID);
//            if (!string.IsNullOrEmpty(updateUserDto.Password))
//            {
//                var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
//                response = await _userManager.ResetPasswordAsync(user, passwordResetToken, updateUserDto.Password);

//                if (!response.Succeeded)
//                {
//                    await _userEditHelper.UndoChanges(userCacheDto);
//                    return BadRequest(Response<NoContent>.Fail(400, response.Errors.Select(x => x.Description).ToList()));
//                }

//            }

//            var allRoles = _roleManager.Roles.ToList();
//            foreach (var role in allRoles)
//            {
//                await _userManager.RemoveFromRoleAsync(user, role.Name);
//            }

//            var newRole = await _roleManager.FindByNameAsync(updateUserDto.RoleName);
//            response = await _userManager.AddToRoleAsync(user, newRole.Name);
//            if (!response.Succeeded)
//            {
//                await _userEditHelper.UndoChanges(userCacheDto);
//                return BadRequest(Response<NoContent>.Fail(400, response.Errors.Select(x => x.Description).ToList()));
//            }

//            return NoContent();

//        }
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> RemoveUser(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            if (user is null)
//                return NotFound(Response<NoContent>.Fail(404, "Kullanıcı Mevcut Değil"));
//            await _userManager.DeleteAsync(user);
//            return Ok(Response<NoContent>.Success(200));
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetUser()
//        {
//            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
//            if (userIdClaim is null)
//                return BadRequest();
//            var user = await _userManager.FindByIdAsync(userIdClaim.Value);

//            if (user is null)
//                return BadRequest();
//            var role = await _userManager.GetRolesAsync(user);

//            return Ok(new UserDto() { Id = user.Id, 
//                UserName = user.UserName, 
//                Email = user.Email, 
//                City = user.City,
//                RoleName = role.FirstOrDefault(), 
//                IsConfirmed = 
//                user.EmailConfirmed,
//                Address=user.Address,
//                Country=user.Country,
//                County=user.County,
//                Gender=user.Gender,
//                MainLanguage=user.MainLanguage,
//                Name=user.Name,
//                PhoneNumber=user.PhoneNumber,
//                PostalCode=user.PhoneNumber,
//                SurName=user.SurName });
//        }
//        //    [Authorize(Roles = "admin")]
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetUserRole(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);

//            var role = await _userManager.GetRolesAsync(user);
//            return Ok(new { Role = role.FirstOrDefault() });
//        }
//        [HttpGet]
//        //       [Authorize(Roles = "admin")]
//        public IActionResult GetAllRoles()
//        {
//            var orgroles = _roleManager.Roles.ToList();
//            var roles = from r in orgroles
//                        select new
//                        {
//                            ID = r.Id,
//                            Name = r.Name
//                        };
//            return Ok(roles);
//        }
//        [HttpGet("{id}")]
//        //        [Authorize(Roles = "admin")]
//        public async Task<IActionResult> GetUser(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);

//            if (user is null)
//                return NotFound();
//            var role = await _userManager.GetRolesAsync(user);
//            return Ok(new UserDto()
//            {
//                Id = user.Id,
//                UserName = user.UserName,
//                Email = user.Email,
//                City = user.City,
//                RoleName = role.FirstOrDefault(),
//                IsConfirmed =
//                           user.EmailConfirmed,
//                Address = user.Address,
//                Country = user.Country,
//                County = user.County,
//                Gender = user.Gender,
//                MainLanguage = user.MainLanguage,
//                Name = user.Name,
//                PhoneNumber = user.PhoneNumber,
//                PostalCode = user.PhoneNumber,
//                SurName = user.SurName
//            });
//        }
//        [HttpGet("{mail}")]

//        public async Task<IActionResult> GetUserByMail(string mail)
//        {
//            var user = await _userManager.FindByEmailAsync(mail);

//            if (user is null)
//                return NotFound(Response<UserDto>.Fail(404, "Kullanıcı Bulunamadı"));
//            var role = await _userManager.GetRolesAsync(user);
//            return Ok(new UserDto()
//            {
//                Id = user.Id,
//                UserName = user.UserName,
//                Email = user.Email,
//                City = user.City,
//                RoleName = role.FirstOrDefault(),
//                IsConfirmed =
//                           user.EmailConfirmed,
//                Address = user.Address,
//                Country = user.Country,
//                County = user.County,
//                Gender = user.Gender,
//                MainLanguage = user.MainLanguage,
//                Name = user.Name,
//                PhoneNumber = user.PhoneNumber,
//                PostalCode = user.PhoneNumber,
//                SurName = user.SurName
//            });
//        }
//        [HttpGet]
//        //       [Authorize(Roles ="admin")]
//        public async Task<IActionResult> GetAllUsers()
//        {
//            var admins = await _userManager.GetUsersInRoleAsync("admin");
//            var clients = await _userManager.GetUsersInRoleAsync("client");
//            var mentors = await _userManager.GetUsersInRoleAsync("mentor");
//            var userList = new List<UserListDto>();
//            foreach (var admin in admins)
//            {
//                userList.Add(new UserListDto()
//                {
//                    Email = admin.Email,
//                    Id = admin.Id,
//                    RoleName = "admin",
//                    UserName = admin.UserName,
//                    Country=admin.Country,
//                    MainLanguage=admin.MainLanguage,
//                    Name=admin.Name,
//                    SurName=admin.SurName
//                });
//            }
//            foreach (var client in clients)
//            {
//                userList.Add(new UserListDto()
//                {
//                    Email = client.Email,
//                    Id = client.Id,
//                    RoleName = "client",
//                    UserName = client.UserName,
//                    Country=client.Country,
//                    MainLanguage=client.MainLanguage,
//                    Name=client.Name,
//                    SurName=client.SurName
//                });
//            }
//            foreach(var mentor in mentors)
//            {
//                userList.Add(new UserListDto()
//                {
//                    Email = mentor.Email,
//                    Id = mentor.Id,
//                    RoleName = "mentor",
//                    UserName = mentor.UserName,
//                    Country = mentor.Country,
//                    MainLanguage = mentor.MainLanguage,
//                    Name = mentor.Name,
//                    SurName = mentor.SurName
//                });
//            }
//            return Ok(userList);
//        }
//        [HttpPost]
//        public async Task<IActionResult> GenerateUserConfirmToken(UserConfirmationTokenDto userConfirmationTokenDto)
//        {
//            var user = await _userManager.FindByIdAsync(userConfirmationTokenDto.Id);
//            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
//            return Ok(Response<UserConfirmationTokenDto>.Success(200, new UserConfirmationTokenDto
//            {
//                Email = user.Email,
//                Id = userConfirmationTokenDto.Id,
//                Token = token,
//                UserName = user.UserName
//            }));
//        }
//        [HttpPost]
//        [AllowAnonymous]
//        public async Task<IActionResult> ConfirmUserToken(UserConfirmationTokenInputDto userConfirmationTokenInputDto)
//        {
//            var user = await _userManager.FindByEmailAsync(userConfirmationTokenInputDto.Email);
//            if (user is null)
//                return NotFound(Response<NoContent>.Fail(404, "Kullanıcı Bulunamadı"));

//            var result = await _userManager.ConfirmEmailAsync(user, userConfirmationTokenInputDto.Token);
//            if (!result.Succeeded)
//            {
//                List<string> errors = new List<string>();
//                foreach (var error in result.Errors)
//                {
//                    errors.Add(error.Description);
//                }
//                return BadRequest(Response<NoContent>.Fail(400, errors));
//            }

//            return Ok(Response<NoContent>.Success(200));
//        }
//        [HttpPost]
//        [AllowAnonymous]
//        public async Task<IActionResult> GeneratePassWordResetToken(ForgotPasswordInputDto forgotPasswordInputDto)
//        {

//            var user = await _userManager.FindByEmailAsync(forgotPasswordInputDto.Email);
//            if (user is null)
//            {
//                return BadRequest(Response<PasswordResetTokenDto>.Fail(400, "E-Mail Sistemde Mevcut Değil"));
//            }
//            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

//            return Ok(Response<PasswordResetTokenDto>.Success(200, new PasswordResetTokenDto()
//            {
//                Email = forgotPasswordInputDto.Email,
//                Token = token,
//                Username = user.UserName
//            }));
//        }
//        [HttpPost]
//        [AllowAnonymous]
//        public async Task<IActionResult> ChangePassWordWithToken(ResetPasswordInputDto resetPasswordInputDto)
//        {
//            var user = await _userManager.FindByEmailAsync(resetPasswordInputDto.Email);
//            if (user is null)
//                return NotFound(Response<NoContent>.Fail(404, "Kullanıcı Bulunamadı"));

//            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordInputDto.Token, resetPasswordInputDto.Password);
//            if (!resetPassResult.Succeeded)
//            {
//                List<string> errors = new List<string>();
//                foreach (var error in resetPassResult.Errors)
//                {
//                    errors.Add(error.Description);
//                }
//                return BadRequest(Response<NoContent>.Fail(400, errors));
//            }

//            return Ok(Response<NoContent>.Success(200));
//        }
//        [HttpPost]
//        public async Task<IActionResult> ChangePassword(UserChangePasswordInput userChangePasswordInput)
//        {
//            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
//            if (userIdClaim is null)
//                return BadRequest();
//            var user = await _userManager.FindByIdAsync(userIdClaim.Value);

//            if (user is null)
//                return BadRequest();

//            var result = await _userManager.ChangePasswordAsync(user, userChangePasswordInput.OldPassword, userChangePasswordInput.NewPassword);

//            if (!result.Succeeded)
//            {
//                var errors = new List<string>();
//                foreach (var error in result.Errors.Select(x => x.Description))
//                {
//                    errors.Add(error);
//                }
//                return BadRequest(errors);
//            }
//            return Ok();
//        }
//    }
//}
