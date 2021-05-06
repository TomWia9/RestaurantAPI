using Application.Common.Models;
using Domain.Requests;
using MediatR;

namespace Application.Auth.Commands.SignUp
{
    public class SignUpCommand : IRequest<AuthenticationResponse>
    {
        public SignUpCommand(UserSignUpRequest userSignUpRequest)
        {
            UserSignUpRequest = userSignUpRequest;
        }

        public UserSignUpRequest UserSignUpRequest { get; set; }
    }
}