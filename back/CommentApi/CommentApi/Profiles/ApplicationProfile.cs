using AutoMapper;
using CommentApi.Requests;
using CommentApi.Responses;
using CommentBusinessLogic.DTO;

namespace CommentApi.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<commentPostRequest, CommentDto>();
            CreateMap<CommentDto, commentResponse>();

        } 
    }
}
