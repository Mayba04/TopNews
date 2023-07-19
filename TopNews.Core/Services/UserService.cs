using AutoMapper;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Login;
using TopNews.Core.DTOs.User;
using TopNews.Core.Entities.User;

namespace TopNews.Core.Services
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
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

            SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
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
            List<UserDTO> mappedUsers = users.Select(u => _mapper.Map<AppUser,UserDTO>(u)).ToList();

            for (int j = 0; j < users.Count; j++)
            {
                var userDto = _mapper.Map<AppUser, UserDTO>(users[j]);
                var roles = await _userManager.GetRolesAsync(users[j]);
                for (var i = 0; i < roles.Count; i++)
                {
                    if (j == i)
                    {
                        userDto.Role = roles[i];
                    }
                }
                mappedUsers[j] = userDto;
            }
            //write code here!

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
        }

        public async Task<ServiceResponse> UpdatePasswordAsync(UpdatePasswordDTO model)
        {
            var user  = await _userManager.FindByIdAsync(model.Id);
            if(user == null) 
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User or password incorrect."
                };
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if(result.Succeeded) 
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

    }
}
