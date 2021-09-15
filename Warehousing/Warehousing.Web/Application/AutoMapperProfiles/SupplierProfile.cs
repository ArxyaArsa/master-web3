using AutoMapper;
using Warehousing.Domain.AggregateModels.SupplierAggregate;
using Warehousing.Web.Models.DTOs;

namespace Warehousing.Web.Application.AutoMapperProfiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierDTO>();
        }
    }
}
