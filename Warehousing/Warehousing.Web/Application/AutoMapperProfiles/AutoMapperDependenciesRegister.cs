using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Warehousing.Web.Application.AutoMapperProfiles
{
    public static class AutoMapperDependenciesRegister
    {
        public static void AddAutoMapperDependencies(this IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new WarehouseLotProfile());
                mc.AddProfile(new ParcelProfile());
                mc.AddProfile(new ParcelTypeProfile()); 
                mc.AddProfile(new SupplierProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }
    }
}
