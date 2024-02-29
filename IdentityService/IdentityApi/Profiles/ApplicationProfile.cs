using AutoMapper;
using IdentityApi.Requests;
using IdentityApi.Responses;
using IdentityBusinessLogic.DTO;
namespace IdentityApi.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<UserRegisterRequest, UserDto>();
            CreateMap<UserDto,UserResponse>();
            CreateMap<UserLoginRequest, UserDto>();
        }
    }
}
