using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Requests;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResponse> Register(UserSignUpRequest userSignUpRequest);
        Task<AuthenticationResponse> Login(UserLoginRequest userLoginRequest);
    }
}