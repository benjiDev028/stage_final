using AutoMapper;
using IdentityBusinessLogic.DTO;
using IdentityDataAccess.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityBusinessLogic.Profiles
{
    public class MappingProfile : Profile
    {   
        public MappingProfile() 

        {
            //comment faire passser les donnees entre les classes  
            CreateMap<UserDto, User>().ReverseMap();
            
        }
    }
}
