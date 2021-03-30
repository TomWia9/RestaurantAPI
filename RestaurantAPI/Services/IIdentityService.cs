using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Data.Requests;
using RestaurantAPI.Data.Response;
using RestaurantAPI.Models.Auth;

namespace RestaurantAPI.Services
{
    public interface IIdentityService
    {
        Task<IdentityResult> Register(UserSignUpRequest userSignUpRequest);
        Task<AuthenticationResponse> Login(UserLoginRequest userLoginRequest);
    }
}
