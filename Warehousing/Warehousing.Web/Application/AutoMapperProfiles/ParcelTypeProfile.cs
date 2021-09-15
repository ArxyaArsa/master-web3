using AutoMapper;
using System.Linq;
using Warehousing.Domain.AggregateModels.WarehouseLotAggregate;
using Warehousing.Web.Models.DTOs;

namespace Warehousing.Web.Application.AutoMapperProfiles
{
    public class ParcelTypeProfile : Profile
    {
        public ParcelTypeProfile()
        {
            CreateMap<ParcelType, ParcelTypeDTO>()
                .ForMember(d => d.ParcelCount, a => a.MapFrom(s => s.GetValidParcels().Count));
        }
    }
}
