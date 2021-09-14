using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehousing.Domain.AggregateModels.WarehouseLotAggregate;
using Warehousing.Web.Models.DTOs;

namespace Warehousing.Web.Application.AutoMapperProfiles
{
    public class WarehouseLotProfile : Profile
    {
        public WarehouseLotProfile()
        {
            CreateMap<WarehouseLot, WarehouseLotDTO>();
        }
    }
}
