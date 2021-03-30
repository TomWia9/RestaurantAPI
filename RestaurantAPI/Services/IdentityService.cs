using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Data.Requests;
using RestaurantAPI.Data.Response;
using RestaurantAPI.Models.Auth;
using RestaurantAPI.Settings;

namespace RestaurantAPI.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;

        public IdentityService(UserManager<User> userManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<IdentityResult> Register(UserSignUpRequest userSignUpRequest)
        {
            var newUser = new User()
            {
                Email = userSignUpRequest.Email,
                UserName = userSignUpRequest.Email,
                FirstName = userSignUpRequest.FirstName,
                LastName = userSignUpRequest.LastName
            };

           var createdUserResult = await _userManager.CreateAsync(newUser, userSignUpRequest.Password);

           if (!createdUserResult.Succeeded)
           {
               return createdUserResult;
           }

           var addedRoleResult = await _userManager.AddToRoleAsync(newUser, "User");

           if (!createdUserResult.Succeeded)
           {
               return addedRoleResult;
           }

           return IdentityResult.Success;

        }

        public async Task<AuthenticationResponse> Login(UserLoginRequest userLoginRequest)
        {
            var user = await _userManager.FindByEmailAsync(userLoginRequest.Email);

            if (user == null)
            {
                return new AuthenticationResponse()
                {
                    Success = false,
                    ErrorMessages = new List<string> { "User not found" }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, userLoginRequest.Password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResponse()
                {
                    Success = false,
                    ErrorMessages = new List<string> {"Login or password incorrect"}
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            return GenerateAuthenticationResponseWithToken(user, roles);

        }

        private AuthenticationResponse GenerateAuthenticationResponseWithToken(User user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Issuer = _jwtSettings.Issuer
            };

            tokenDescriptor.Subject.AddClaims(roleClaims);

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResponse()
            {
                Email = user.Email,
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }


    }
}
