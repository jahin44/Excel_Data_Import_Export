using AutoMapper;
using DataImporter.SystemImporter.BusinessObjects;
using DataImporter.Web.Models.GroupModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateGroupModel, Group>().ReverseMap();
            CreateMap<EditGroupModel, Group>().ReverseMap();
           
        }
    }
}
