using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehousing.Web.Application.AutoMapperProfiles
{
    public static class AutoMapperDependenciesRegister
    {
        public static void AddAutoMapperDependencies(this IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new WarehouseLotProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }
    }
}
