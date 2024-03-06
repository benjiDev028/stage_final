using IdentityBusinessLogic.DTO;

namespace IdentityBusinessLogic.Services.Interfaces
{
    public interface ICredentialsService
    {
        /// <summary>
        /// Method responsible for creating a password hash and a password salt that will be saved
        /// in the database.
        /// </summary>
        /// <param name="password">Entered password by the user</param>
        /// <param name="passwordHash">passwordHash</param>
        /// <param name="passwordSalt">passwordSalt</param>
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        /// <summary>
        /// Method responsible for setting the hash and salt password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        void SetPasswordHash(UserDto user, string password);

        /// <summary>
        /// Method responsible for authenticating the password 
        /// </summary>
        /// <param name="password">Entered password by the user</param>
        /// <param name="passwordHash">PasswordHash</param>
        /// <param name="passwordSalt">PasswordSalt</param>
        /// <returns>returns true if the password is authentic and false in the other case</returns>
        bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);

        /// <summary>
        /// Method responsible for creating a jwt token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string CreateToken(UserDto user);

        /// <summary>
        /// Method responsible for creating a jwt refresh token
        /// </summary>
        /// <returns></returns>
        string CreateRefreshToken();
    }
}
