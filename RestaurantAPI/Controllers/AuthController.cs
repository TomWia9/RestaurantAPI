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

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthController(IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp(UserSignUpRequest userSignUpRequest)
        {
            var user = _mapper.Map<UserSignUpRequest, User>(userSignUpRequest);

            var userCreateResult = await _userManager.CreateAsync(user, userSignUpRequest.Password);

            if (userCreateResult.Succeeded)
            {
                return Created(string.Empty, string.Empty);
            }

            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(UserLoginRequest userLoginRequest)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userLoginRequest.Email);

            if (user is null)
            {
                return NotFound("User not found");
            }

            var userSignInResult = await _userManager.CheckPasswordAsync(user, userLoginRequest.Password);

            if (userSignInResult)
            {
                return Ok();
            }

            return BadRequest("Email or password incorrect");
        }

        //[HttpPost("User/{userEmail}/Role")]
        //public async Task<IActionResult> AddUserToRole(string userEmail, [FromBody] string roleName)
        //{
        //    var user = _userManager.Users.SingleOrDefault(u => u.UserName == userEmail);

        //    var result = await _userManager.AddToRoleAsync(user, roleName);

        //    if (result.Succeeded)
        //    {
        //        return Ok();
        //    }

        //    return Problem(result.Errors.First().Description, null, 500);
        //}
    }
}
