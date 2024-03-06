using IdentityBusinessLogic.DTO;
using IdentityDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityBusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(Guid id);
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<bool> CreateAsync(UserDto user);
        Task<UserDto> UpdateAsync(UserDto user);
        Task<bool> UpdatePasswordAsync(Guid id, byte[] hashPassword, byte[] saltPassword);
        Task<bool> DeleteAsync(Guid id);
    }
}
