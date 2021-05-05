using Application.Common.Models;
using Domain.Requests;
using MediatR;

namespace Application.Auth.Commands.Login
{
    public class LoginCommand : IRequest<AuthenticationResponse>
    {
        public UserLoginRequest UserLoginRequest { get; set; }

        public LoginCommand(UserLoginRequest userLoginRequest)
        {
            UserLoginRequest = userLoginRequest;
        }
    }
}
