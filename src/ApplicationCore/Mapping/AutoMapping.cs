using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CoreTemplate.ApplicationCore.Entities;
using CoreTemplate.ApplicationCore.Models;
using CoreTemplate.ApplicationCore.Specifications;

namespace CoreTemplate.ApplicationCore.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Item, ItemViewModel>().ReverseMap();
        }

    }
}
