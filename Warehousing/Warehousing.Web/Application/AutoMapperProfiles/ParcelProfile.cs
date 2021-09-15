using AutoMapper;
using Warehousing.Domain.AggregateModels.WarehouseLotAggregate;
using Warehousing.Web.Models.DTOs;

namespace Warehousing.Web.Application.AutoMapperProfiles
{
    public class ParcelProfile : Profile
    {
        public ParcelProfile()
        {
            CreateMap<Parcel, ParcelDTO>();
        }
    }
}
