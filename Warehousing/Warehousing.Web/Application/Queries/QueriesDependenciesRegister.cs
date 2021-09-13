using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Warehousing.Web.Application.Queries
{
    public static class QueriesDependenciesRegister
    {
        public static void AddQueriesDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped(c => new WarehouseQueries(configuration.GetConnectionString("WarehousingConnection")));
        }
    }
}
