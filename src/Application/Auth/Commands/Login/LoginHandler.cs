using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Auth.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthenticationResponse>
    {
        private readonly IIdentityService _identityService;

        public LoginHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<AuthenticationResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.Login(request.UserLoginRequest);

            if (result.ErrorMessages.Any())
            {
                throw new BadRequestException(string.Join(Environment.NewLine, result.ErrorMessages));
            }

            return result;
        }
    }
}
