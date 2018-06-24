using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.DTO;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDto, Customer>().ForMember(src => src.Id, opt => opt.Ignore());
            CreateMap<Customer, CustomerDto>();//.ForMember(src => src.Id, opt => opt.Ignore());
            CreateMap<MovieDto, Movie>().ForMember(src => src.Id, opt => opt.Ignore());
            CreateMap<Movie, MovieDto>();
            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<Genere, GenereDto>();
        }
    }
}