using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Data.Requests;
using RestaurantAPI.Models.Auth;

namespace RestaurantAPI.Services
{
    public interface IIdentityService
    {
        Task<bool> Register(UserSignUpRequest userSignUpRequest);
        Task<bool> Login(UserLoginRequest userLoginRequest);
        Task<bool> UserExists(string email);
    }
}
