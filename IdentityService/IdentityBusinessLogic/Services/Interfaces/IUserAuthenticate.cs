using IdentityBusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityBusinessLogic.Services.Interfaces
{
    public interface IUserAuthenticate
    {
        Task<(string accessToken, string refreshToken)> AuthenticateUserAsync(string email, string password);

        Task<(string accessToken, string refreshToken)> CreateTokensAsync(string refreshToken);

        Task<(string accessToken, string refreshToken)> RegisterUserAsync(UserDto user, string password);
    }
}
