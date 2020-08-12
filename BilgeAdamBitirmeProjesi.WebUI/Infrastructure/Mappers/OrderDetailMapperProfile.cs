using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.OrderDetail;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.OrderDetailViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Mappers
{
    public class OrderDetailMapperProfile : Profile
    {
        public OrderDetailMapperProfile()
        {
            //Neyi neye mappleyeceğimi gösterdiğim kısım.
            //Null geldiğinde veritabanına kabul et
            //null gelmediği zaman Dto kabul et
            //Reversemap tam tersi

            CreateMap<OrderDetailViewModel, OrderDetailRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));

            CreateMap<OrderDetailViewModel, OrderDetailResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));


        }
    }
}
