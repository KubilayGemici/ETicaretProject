using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.Cart;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.CartViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Mappers
{
    public class CartMapperProfile : Profile
    {
        public CartMapperProfile()
        {
            CreateMap<CartViewModel, CartRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));

            CreateMap<CartViewModel, CartResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));
        }
    }
}
