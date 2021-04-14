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

            if (!result.Success)
            {
                throw new BadRequestException(string.Join(Environment.NewLine, result.ErrorMessages));
            }

            return result;
        }
    }
}
