using IdentityDataAccess.Models;
using IdentityDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityDataAccess.Repositories.Implementations
{
    public class UserRepository : IRepository
    {
        //DI du context
        private readonly  UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<bool> CreateAsync(User user)
        {
            _userContext.Users.Add(user);

            await _userContext.SaveChangesAsync();

            return true;

        }
        public async Task<List<User>> GetAllAsync()
        {
            

            return await _userContext.Users.ToListAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var deletedUser = await _userContext.Users.FindAsync(id);

            _userContext.Users.Remove(deletedUser);

            await _userContext.SaveChangesAsync();

            return true;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userContext.Users
                .Where(u => u.Email.Equals(email))
                .FirstOrDefaultAsync();
        }

        public  async Task<User> GetByIdAsync(Guid id)
        {
            var retrievedUser = await _userContext.Users.FirstOrDefaultAsync(user => user.Id == id);

            return retrievedUser;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var updateUser =  await _userContext.Users.Where(u => u.Id.Equals(u.Id)).FirstAsync();
            


            updateUser.FirstName = user.FirstName;
            updateUser.LastName = user.LastName;
            updateUser.Email = user.Email;
            updateUser.Username = user.Username;



            await _userContext.SaveChangesAsync();
            return updateUser;
        }

        public async Task<bool> UpdatePasswordAsync(Guid id, byte[] hashPassword, byte[] saltPassword)
        {
            var entityToUpdate = await _userContext.Users.FindAsync(id);

            entityToUpdate.PasswordHash = hashPassword;
            entityToUpdate.PasswordSalt = saltPassword;

            await _userContext.SaveChangesAsync();

            return true;
        }

       
    }
}
