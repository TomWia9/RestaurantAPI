using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Commands.Auth;
using RestaurantAPI.Data.Response;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Services;

namespace RestaurantAPI.Handlers.Auth
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

            if (!result.Success)
            {
                throw new BadRequestException(string.Join(Environment.NewLine, result.ErrorMessages));
            }

            return result;
        }
    }
}
