using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Data.Requests;
using RestaurantAPI.Models.Auth;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public AuthController(IMapper mapper, IIdentityService identityService)
        {
            _mapper = mapper;
            _identityService = identityService;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp(UserSignUpRequest userSignUpRequest)
        {
            var result = await _identityService.Register(userSignUpRequest);

            if (result.Succeeded)
            {
                return Created(string.Empty, string.Empty);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(UserLoginRequest userLoginRequest)
        {
            var result = await _identityService.Login(userLoginRequest);

            if (result == true)
            {
                return Ok();
            }

            return BadRequest("Email or password incorrect");
        }
    }
}
