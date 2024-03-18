using AutoMapper;
using IdentityBusinessLogic.DTO;
using IdentityBusinessLogic.Exceptions;
using IdentityBusinessLogic.Services.Interfaces;
using IdentityDataAccess.Models;
using IdentityDataAccess.Repositories.Interfaces;

namespace IdentityBusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(UserDto user)
        {
            
                //to longwe na object ya type dto toke na object ya user(model, na dataAcceess)
                var userToCreate = _mapper.Map<User>(user);
                var VerifiedEmail = await _repository.GetUserByEmailAsync(user.Email);
                var VerifiedUsername = await _repository.GetUserByUsernameAsync(user.Username);

                if (VerifiedEmail != null )
                {
                    throw new AlreadyExistsException("email already Exist");
                    
                }
                if(VerifiedUsername != null)
                {
                    throw new AlreadyExistsException("username already Exist");
                }
                
                var isCreated = await _repository.CreateAsync(userToCreate);
                
               return isCreated;
            
            throw new ImpossibleException("impossible de creer un user");
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            
                var userToDeleted = await _repository.DeleteAsync(id);

                if (userToDeleted == null)
                {
                    throw new NotFoundException( "there is not  user with this id ");
                }
                return userToDeleted;
            
           
         
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            
                var userFound = await _repository.GetByIdAsync(id);
                if (userFound is null)
                {
                    throw new NotFoundException(" le user n'existe pas ");
                }
                return _mapper.Map<UserDto>(userFound);
            
           
            
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _repository.GetUserByEmailAsync(email);

            if (user is null)
                throw new NotFoundException("There is no user with such email");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(UserDto user)
        {
           
                var existingUser = await _repository.GetByIdAsync(user.Id);
                if (existingUser is null)
                {
                    throw new NotFoundException("Utilisateur avec cet email n'existe pas.");
                }

                // Mettez à jour les propriétés de l'utilisateur existant
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Username = user.Username;
                

                var updatedUser = await _repository.UpdateAsync(existingUser);

                return _mapper.Map<UserDto>(updatedUser);
            
            
                throw new ImpossibleException("Impossible de mettre à jour l'utilisateur.");
            
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            
             
                var users =  await _repository.GetAllAsync();
                return  _mapper.Map<List<UserDto>>(users);


            throw new ImpossibleException(" impossible de get tous les users");  
           
            
        }

        public Task<bool> UpdatePasswordAsync(Guid id, byte[] hashPassword, byte[] saltPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var userUsername = await _repository.GetUserByUsernameAsync(username);

            if (userUsername != null)
                throw new NotFoundException("username deja utilisE");

            return _mapper.Map<UserDto>(userUsername);
        }
    }
}
