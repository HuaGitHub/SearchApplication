using AutoMapper;
using SearchApplication.Data.Classes;
using SearchApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchApplication.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactViewModel>().ReverseMap();
        }
    }
}
