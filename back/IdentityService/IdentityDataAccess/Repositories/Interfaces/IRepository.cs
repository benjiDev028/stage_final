using IdentityDataAccess.Models;

namespace IdentityDataAccess.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<List<User>> GetAllAsync();
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> UpdatePasswordAsync(Guid id, byte[] hashPassword, byte[] saltPassword);
        Task<bool> DeleteAsync(Guid id);
        
    }
}
