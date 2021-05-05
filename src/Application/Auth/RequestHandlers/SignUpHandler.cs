using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Auth.Commands.SignUp;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Auth.RequestHandlers
{
    public class SignUpHandler : IRequestHandler<SignUpCommand, AuthenticationResponse>
    {
        private readonly IIdentityService _identityService;

        public SignUpHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        public async Task<AuthenticationResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.Register(request.UserSignUpRequest);

            if (result.ErrorMessages.Any())
            {
                throw new BadRequestException(string.Join(Environment.NewLine, result.ErrorMessages));
            }

            return result;
        }
    }
}
