using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.ProductViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Extensions;

namespace BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Mappers
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<CreateProductViewModel, ProductRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));

            CreateMap<UpdateProductViewModel, ProductRequest>()
               .ReverseMap()
               .IgnoreAllNonExisting()
               .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));

            CreateMap<ProductViewModel, ProductRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));

            CreateMap<CreateProductViewModel, ProductResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));

            CreateMap<UpdateProductViewModel, ProductResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));

            CreateMap<ProductViewModel, ProductResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));
        }
    }
}
