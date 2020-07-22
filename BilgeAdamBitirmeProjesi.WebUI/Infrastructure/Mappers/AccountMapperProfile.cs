using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Extensions;
using BilgeAdamBitirmeProjesi.WebUI.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Mappers
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            //Neyi neye mappleyeceğimi gösterdiğim kısım.
            //Null geldiğinde veritabanına kabul et
            //null gelmediği zaman Dto kabul et
            //Reversemap tam tersi

            CreateMap<LoginViewModel, UserRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));

            CreateMap<LoginViewModel, UserResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));


        }
    }
}
