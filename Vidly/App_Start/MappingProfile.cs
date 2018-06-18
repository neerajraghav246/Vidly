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
            CreateMap<Customer, CustomerDto>();//.ForMember(src => src.Id, opt => opt.Ignore());
            CreateMap<CustomerDto, Customer>();//.ForMember(src => src.Id, opt => opt.Ignore());
        }
    }
}