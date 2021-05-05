using System.Threading.Tasks;
using Application.Auth.Commands.Login;
using Application.Auth.Commands.SignUp;
using Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
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

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="userSignUpRequest">The user to register</param>
        /// <returns>An IActionResult</returns>
        /// <response code="201">Creates user and returns jwt token</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp(UserSignUpRequest userSignUpRequest)
        {
            var result = await _mediator.Send(new SignUpCommand(userSignUpRequest));
            return Created(string.Empty, result);
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="userLoginRequest">The user to authenticate</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Authenticates user and returns jwt token</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(UserLoginRequest userLoginRequest)
        {
            var result = await _mediator.Send(new LoginCommand(userLoginRequest));
            return Ok(result);
        }
    }
}
