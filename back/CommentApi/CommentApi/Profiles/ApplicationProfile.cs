using AutoMapper;
using CommentApi.Requests;
using CommentBusinessLogic.DTO;

namespace CommentApi.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<commentPostRequest, CommentDto>();
        } 
    }
}
