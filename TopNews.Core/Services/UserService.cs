using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Login;
using TopNews.Core.DTOs.User;
using TopNews.Core.Entities.User;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TopNews.Core.Services
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly EmailServices _emailService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IConfiguration configuration, EmailServices emailServices)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailService = emailServices;
        }

        public async Task<ServiceResponse> LoginUserAsync(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User or password incorrect."
                };
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);/// failed login user
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, model.RememberMe);

                return new ServiceResponse
                {
                    Success = true,
                    Message = "User successfully logged in."
                };
            }

            if (result.IsNotAllowed)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Confirm your email please."
                };
            }

            if (result.IsLockedOut)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User is locked. Connect with your site administrator."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "User or password incorrect."
            };
        }

        public async Task<ServiceResponse> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return new ServiceResponse
            {
                Success = true,
                Message = "false"
            };
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();

            List<UserDTO> mappedUsers = users.Select(u => _mapper.Map<AppUser, UserDTO>(u)).ToList();

            for (int i = 0; i < users.Count; i++)
            {
                mappedUsers[i].Role = (await _userManager.GetRolesAsync(users[i])).FirstOrDefault();
            }

            return new ServiceResponse
            {
                Success = true,
                Message = "All users loaded.",
                Payload = mappedUsers
            };
        }

        public async Task<ServiceResponse> GetUserByIdAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User or password incorrect."
                };
            }
            var roles = await _userManager.GetRolesAsync(user);

            var mappedUser = _mapper.Map<AppUser, UpdateUserDTO>(user);

            mappedUser.Role = roles[0];
            return new ServiceResponse
            {
                Success = true,
                Message = "User successfully loaded",
                Payload = mappedUser
            };
            ////////////////
        }

        public async Task<ServiceResponse> UpdatePasswordAsync(UpdatePasswordDTO model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User or password incorrect."
                };
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();

                return new ServiceResponse
                {
                    Success = true,
                    Message = "Passsword successfully updated."
                };
            }



            List<IdentityError> errorList = result.Errors.ToList();
            string errors = "";
            foreach (var error in errorList)
            {
                errors = errors + error.Description.ToList();
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "Error",
                Payload = errors
            };
        }

        public async Task<ServiceResponse> ChangeMainInfoUserAsync(UpdateUserDTO newinfo)
        {
            AppUser user = await _userManager.FindByIdAsync(newinfo.Id);

            if (user != null)
            {
                user.FirstName = newinfo.FirstName;
                user.LastName = newinfo.LastName;
                user.Email = newinfo.Email;
                user.PhoneNumber = newinfo.PhoneNumber;

                IdentityResult result = await _userManager.UpdateAsync(user);

                return (result.Succeeded) ?
                    new ServiceResponse
                    {
                        Success = true,
                        Message = "The information has been changed"
                    } :
                    new ServiceResponse
                    {
                       Success = false,
                        Message = "Error",
                        Payload = result.Errors
                    };
            }
            return new ServiceResponse 
            {
                Success = false,
                Message = "Error"
            };
        }

        public async Task<ServiceResponse> CreateUserAsync(CreateUserDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return new ServiceResponse
                {
                    Message = "User exists.",
                    Success = false,
                };
            }

            var mappedUser = _mapper.Map<CreateUserDTO, AppUser>(model);
           
            var acount = await _userManager.CreateAsync(mappedUser, model.Password);

            if (!acount.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Error creating"
                };
            }
            
            var roles = await _userManager.AddToRoleAsync(mappedUser, model.Role);

           await SendConfirmationEmailAsync(mappedUser);

            if (!roles.Succeeded) 
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Error creating"
                };
            }
            List<IdentityError> errorList = roles.Errors.ToList();
            string errors = "";
            foreach (var error in errorList)
            {
                errors = errors + error.Description.ToList();
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "Error",
                Payload = errors
            };
        }

       
        public async Task<ServiceResponse> DeleteUserAsync(string Id)
        {
            AppUser userdelete = await _userManager.FindByIdAsync(Id);
            if (userdelete != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(userdelete);
                if (result.Succeeded)
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Message = "succesfull",
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = "Error",
                        Payload = result.Errors.Select(e => e.Description)
                    };
                }
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "Success",
            };
        }

        public async Task SendConfirmationEmailAsync(AppUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedToken);

            //string url = $"{_configuration["HostSettings:URL"]}/Dashboard/confirmemail?userId={user.Id}&token={validEmailToken}";
            //var url = $"{_configuration["HostSettings:URL"]}/Dashboard/confirmemail?userid={user.Id}&token={validEmailToken}";
            //string emailBody = $"<h1>Conform your email please.</h1><a href='{url}'>Confirm now!</a>";

            //await _emailServices.SendEmail(user.Email, "Email confirmation.", emailBody);

            var url = $"{_configuration["HostSettings:URL"]}/Dashboard/confirmemail?userid={user.Id}&token={validEmailToken}";

            string emailBody = $"<h1>Confirm your email please.</h1><a href='{url}'>Confirm now!</a>";
            await _emailService.SendEmail(user.Email, "Email confirmation.", emailBody);
        }

        public async Task<ServiceResponse> ForgotPasswordAsync (string email)
        {

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Message = "User exists.",
                    Success = false,
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedToken);

            var url = $"{_configuration["HostSettings:URL"]}/Dashboard/ResetPassword?email={email}&token={validEmailToken}";

            string emailBody = $"<h1>Follow the instruction for reset password.</h1><a href='{url}'>Reset now!</a>";
            await _emailService.SendEmail(email, "Reset password for TopNews.", emailBody);

            return new ServiceResponse
            {
                Success = true,
                Message = "Email successfully sent."
            };

        }



        public async Task<ServiceResponse> ConfirmEmailAsync(string userId,string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message= "User not found",
                };
            }
            var decodedToken =WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);
            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Email successfully confirmed."
                };
            }

            return new ServiceResponse
            {
                Success = true,
                Message = "Email successfully confirmed."
            };
        }
    }
}
