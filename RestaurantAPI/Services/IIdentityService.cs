using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Data.Requests;

namespace RestaurantAPI.Services
{
    public interface IIdentityService
    {
        Task<bool> Register(UserSignUpRequest userSignUpRequest);
        Task<bool> Login(UserSignUpRequest userSignInRequest);
    }
}
