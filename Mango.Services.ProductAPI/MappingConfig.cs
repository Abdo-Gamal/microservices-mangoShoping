using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
namespace Mango.Services.ProductAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<ProductDto,Product>();
            //  CreateMap<invoice, invoicesModelView>()
            //.ForSourceMember(source => source.Total, opt => opt.DoNotValidate())
            //.ReverseMap();

        }
    }
}
