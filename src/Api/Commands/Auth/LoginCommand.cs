using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Data.Requests;
using RestaurantAPI.Data.Response;

namespace RestaurantAPI.Commands.Auth
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
