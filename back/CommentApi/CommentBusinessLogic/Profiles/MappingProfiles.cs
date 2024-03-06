using AutoMapper;
using CommentBusinessLogic.DTO;
using CommentDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentBusinessLogic.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        { 
            CreateMap<CommentDto,Commentaire>().ReverseMap();
        }

    }
}
