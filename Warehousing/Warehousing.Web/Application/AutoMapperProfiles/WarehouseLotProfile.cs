using AutoMapper;
using System.Linq;
using Warehousing.Domain.AggregateModels.WarehouseLotAggregate;
using Warehousing.Web.Models.DTOs;

namespace Warehousing.Web.Application.AutoMapperProfiles
{
    public class WarehouseLotProfile : Profile
    {
        public WarehouseLotProfile()
        {
            CreateMap<WarehouseLot, WarehouseLotDTO>()
                .ForMember(d => d.ParcelCount, a => a.MapFrom(s => s.GetValidParcels().Count))
                .ForMember(d => d.ParcelWeight, a => a.MapFrom(s => s.GetValidParcels().Sum(x => x.Weight ?? 0)));
        }
    }
}
