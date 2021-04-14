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
    public class SignUpCommand : IRequest<AuthenticationResponse>
    {
        public UserSignUpRequest UserSignUpRequest { get; set; }   

        public SignUpCommand(UserSignUpRequest userSignUpRequest)
        {
            UserSignUpRequest = userSignUpRequest;
        }
    }
}
