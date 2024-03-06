using IdentityBusinessLogic.DTO;
using IdentityBusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityBusinessLogic.Services.Implementations
{
    public class UserAuthenticate : IUserAuthenticate
    {
        private readonly IUserService _userService;
        private readonly ICredentialsService _credentialsService;

        public UserAuthenticate(IUserService service, ICredentialsService credentialsService)
        {
            _userService = service;
            _credentialsService = credentialsService;
        }

        public async Task<(string accessToken, string refreshToken)> AuthenticateUserAsync(string email, string password)
        {
            var userFound = await _userService.GetUserByEmailAsync(email);

            if (!_credentialsService.VerifyPassword(password, userFound.PasswordHash, userFound.PasswordSalt))
            {
                throw new Exception("User's credentials are wrong");
            }

            var accessToken = _credentialsService.CreateToken(userFound);
            var refreshToken = _credentialsService.CreateRefreshToken();

            userFound.RefreshToken = refreshToken;
            await _userService.UpdateAsync(userFound);

            return (accessToken, refreshToken);
        }

        public async Task<(string accessToken, string refreshToken)> CreateTokensAsync(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);

            var accessToken = _credentialsService.CreateToken(user);
            var newRefreshToken = _credentialsService.CreateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userService.UpdateAsync(user);

            return (accessToken, newRefreshToken);
        }

        public async Task<(string accessToken, string refreshToken)> RegisterUserAsync(UserDto user, string password)
        {
            _credentialsService.SetPasswordHash(user, password);

            await _userService.CreateAsync(user);

            var accessToken = _credentialsService.CreateToken(user);
            var refreshToken = _credentialsService.CreateRefreshToken();

            return (accessToken, refreshToken);
        }
    }
}
