using AutoMapper;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();

            //  CreateMap<invoice, invoicesModelView>()
            //.ForSourceMember(source => source.Total, opt => opt.DoNotValidate())
            //.ReverseMap();

        }
    }
}
