using AutoMapper;
using BilgeAdamBitirmeProjesi.API.Infrastructure.Extensions;
using BilgeAdamBitirmeProjesi.Common.DTOs.Comment;
using BilgeAdamBitirmeProjesi.Model.Entities;

namespace BilgeAdamBitirmeProjesi.API.Infrastructure.Mapper
{
    public class CommentMapperProfile : Profile
    {
        public CommentMapperProfile()
        {
            //Neyi neye mappleyeceğimi gösterdiğim kısım.
            //Null geldiğinde veritabanına kabul et
            //null gelmediği zaman Dto kabul et
            //Reversemap tam tersi
            CreateMap<Comment, CommentRequest>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));

            CreateMap<Comment, CommentResponse>()
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForAllMembers(options => options.Condition((src, dest, srcmemeber) => srcmemeber != null));
        }
    }
}
