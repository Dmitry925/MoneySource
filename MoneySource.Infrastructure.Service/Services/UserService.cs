using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoneySource.Core.Application.Authentication;
using MoneySource.Core.Application.Interfaces;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MoneySource.Infrastructure.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly JWT _jwt;

        public UserService(UserManager<User> userManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }

        public async Task<AuthorizationModel> GetTokenAsync(AuthenticationModel model)
        {
            var authenticationModel = new AuthorizationModel();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";

                return authenticationModel;
            }
            
            if(await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.UserId = user.Id;
                authenticationModel.isAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = roleList.ToList();

                return authenticationModel;
            }

            authenticationModel.isAuthenticated = false;
            authenticationModel.Message = $"Incorrect credentials for user {user.Email}.";

            return authenticationModel;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            for(int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);


            return jwtSecurityToken;
        }
    }
}
