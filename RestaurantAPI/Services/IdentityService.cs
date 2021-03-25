using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Data.Requests;
using RestaurantAPI.Models.Auth;

namespace RestaurantAPI.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;

        public IdentityService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Register(UserSignUpRequest userSignUpRequest)
        {
            var newUser = new User()
            {
                Email = userSignUpRequest.Email,
                UserName = userSignUpRequest.Email,
                FirstName = userSignUpRequest.FirstName,
                LastName = userSignUpRequest.LastName
            };

           var createdUserResult = await _userManager.CreateAsync(newUser, userSignUpRequest.Password);

           if (!createdUserResult.Succeeded)
           {
               return createdUserResult;
           }

           var addedRoleResult = await _userManager.AddToRoleAsync(newUser, "User");

           if (!createdUserResult.Succeeded)
           {
               return addedRoleResult;
           }

           return IdentityResult.Success;

        }

        public async Task<bool> Login(UserLoginRequest userLoginRequest)
        {
            var user = await _userManager.FindByEmailAsync(userLoginRequest.Email);

            if (user == null)
            {
                return false;
                //TODO it should also return errors
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, userLoginRequest.Password);

            if (userHasValidPassword)
            {
                return true;
                //this method will be returned token in the future
            }

            return false;

        }
    }
}
