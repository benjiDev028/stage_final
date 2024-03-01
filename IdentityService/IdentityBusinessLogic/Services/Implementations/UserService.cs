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
            try
            {
                //to longwe na object ya type dto toke na object ya user(model, na dataAcceess)
                var userToCreate = _mapper.Map<User>(user);
                var isCreated = await _repository.CreateAsync(userToCreate);
                
                return isCreated;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var userToDeleted = await _repository.DeleteAsync(id);

                if (userToDeleted == null)
                {
                    throw new NotFoundException( "there is not  user with this id ");
                }
                return userToDeleted;
            }
            catch
            {
                throw new Exception("user cannot be deteled");
            }
         
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            try
            {
                var userFound = await _repository.GetByIdAsync(id);
                if(userFound == null)
                {
                    throw new NotFoundException(" le user n'existe pas ");
                }
                return _mapper.Map<UserDto>(userFound);
            }
            catch
            {
                throw new Exception(" user GetByid cannot be selected");
            }
            
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _repository.GetUserByEmailAsync(email);

            if (user is null)
                throw new Exception("There is no user with such email");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(UserDto user)
        {
            try
            {
                var userUpdatedMap = _mapper.Map<User>(user);
                var userUpdated = await _repository.UpdateAsync(userUpdatedMap);

                return _mapper.Map<UserDto>(userUpdated);
            }
            catch
            {
                throw new Exception("update impossible");
            }

        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            try
            {
             
                var users =  await _repository.GetAllAsync();
                return  _mapper.Map<List<UserDto>>(users);
                 
             
              
            }catch(Exception e)
            {
                throw e;
            }
        }

        public Task<bool> UpdatePasswordAsync(Guid id, byte[] hashPassword, byte[] saltPassword)
        {
            throw new NotImplementedException();
        }


    }
}
