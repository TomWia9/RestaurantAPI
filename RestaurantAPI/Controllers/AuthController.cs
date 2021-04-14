using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Commands.Auth;
using RestaurantAPI.Data.Requests;
using RestaurantAPI.Models.Auth;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp(UserSignUpRequest userSignUpRequest)
        {
            var result = await _mediator.Send(new SignUpCommand(userSignUpRequest));
            return Created(string.Empty, result);
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(UserLoginRequest userLoginRequest)
        {
            var result = await _mediator.Send(new LoginCommand(userLoginRequest));
            return Ok(result);
        }
    }
}
