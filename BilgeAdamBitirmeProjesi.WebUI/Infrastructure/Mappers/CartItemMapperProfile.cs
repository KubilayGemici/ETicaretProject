using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.CartItem;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.CartItemViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Mappers
{
    public class CartItemMapperProfile : Profile
    {
        public CartItemMapperProfile()
        {
            CreateMap<CartItemViewModel, CartItemRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));

            CreateMap<CartItemViewModel, CartItemResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));
        }
    }
}
