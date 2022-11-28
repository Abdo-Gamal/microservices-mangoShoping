using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
namespace Mango.Services.ShoppingCartAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
           // CreateMap<Product, ProductDto>();

           // CreateMap<ProductDto,Product>();
            //  CreateMap<invoice, invoicesModelView>()
            //.ForSourceMember(source => source.Total, opt => opt.DoNotValidate())
            //.ReverseMap();

        }
    }
}
