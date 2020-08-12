using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.Order;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.OrderViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Mappers
{
    public class OrderMapperProfile : Profile 
    {
        public OrderMapperProfile()
        {
            CreateMap<OrderViewModel, OrderRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));

            CreateMap<OrderViewModel, OrderResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));
        }
    }
}
