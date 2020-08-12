using AutoMapper;
using BilgeAdamBitirmeProjesi.API.Infrastructure.Extensions;
using BilgeAdamBitirmeProjesi.Common.DTOs.OrderDetail;
using BilgeAdamBitirmeProjesi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.API.Infrastructure.Mapper
{
    public class OrderDetailMapperProfile : Profile
    {
        public OrderDetailMapperProfile()
        {
            //Neyi neye mappleyeceğimi gösterdiğim kısım.
            //Null geldiğinde veritabanına kabul et
            //null gelmediği zaman Dto kabul et
            //Reversemap tam tersi
            CreateMap<OrderDetail, OrderDetailRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));
            //Category Response tam tersini yazdık.
            CreateMap<OrderDetail, OrderDetailResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));
        }
    }
}
