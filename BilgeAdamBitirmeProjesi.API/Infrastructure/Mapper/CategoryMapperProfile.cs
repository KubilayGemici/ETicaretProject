using AutoMapper;
using BilgeAdamBitirmeProjesi.API.Infrastructure.Extensions;
using BilgeAdamBitirmeProjesi.Common.DTOs.Category;
using BilgeAdamBitirmeProjesi.Model.Entities;

namespace BilgeAdamBitirmeProjesi.API.Infrastructure.Mapper
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            //Neyi neye mappleyeceğimi gösterdiğim kısım.
            //Null geldiğinde veritabanına kabul et
            //null gelmediği zaman Dto kabul et
            //Reversemap tam tersi
            CreateMap<Category, CategoryRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));
            //Category Response tam tersini yazdık.
            CreateMap<Category, CategoryResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));
        }
    }
}
