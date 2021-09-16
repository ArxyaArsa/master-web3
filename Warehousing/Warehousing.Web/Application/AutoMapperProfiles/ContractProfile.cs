using AutoMapper;
using Warehousing.Domain.AggregateModels.SupplierAggregate;
using Warehousing.Web.Models.DTOs;

namespace Warehousing.Web.Application.AutoMapperProfiles
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<Contract, ContractDTO>()
                .ForMember(d => d.SupplierName, a => a.MapFrom(s => s.Supplier.Name));
        }
    }
}
