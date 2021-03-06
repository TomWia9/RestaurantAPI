using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Settings;
using Domain.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;

        public IdentityService(UserManager<User> userManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthenticationResponse> Register(UserSignUpRequest userSignUpRequest)
        {
            var newUser = new User
            {
                Email = userSignUpRequest.Email,
                UserName = userSignUpRequest.Email,
                FirstName = userSignUpRequest.FirstName,
                LastName = userSignUpRequest.LastName
            };

            var createdUserResult = await _userManager.CreateAsync(newUser, userSignUpRequest.Password);

            if (!createdUserResult.Succeeded)
                return new AuthenticationResponse
                {
                    ErrorMessages = createdUserResult.Errors.Select(e => e.Description)
                };

            var addedRoleResult = await _userManager.AddToRoleAsync(newUser, "User");

            if (!addedRoleResult.Succeeded)
                return new AuthenticationResponse
                {
                    ErrorMessages = addedRoleResult.Errors.Select(e => e.Description)
                };

            return await GenerateAuthenticationResponseWithTokenAsync(newUser);
        }

        public async Task<AuthenticationResponse> Login(UserLoginRequest userLoginRequest)
        {
            var user = await _userManager.FindByEmailAsync(userLoginRequest.Email);

            if (user == null)
                return new AuthenticationResponse
                {
                    ErrorMessages = new List<string> {"User not found"}
                };

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, userLoginRequest.Password);

            if (!userHasValidPassword)
                return new AuthenticationResponse
                {
                    ErrorMessages = new List<string> {"Login or password incorrect"}
                };

            return await GenerateAuthenticationResponseWithTokenAsync(user);
        }

        private async Task<AuthenticationResponse> GenerateAuthenticationResponseWithTokenAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

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
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Issuer = _jwtSettings.Issuer
            };

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            tokenDescriptor.Subject.AddClaims(roleClaims);

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResponse
            {
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}