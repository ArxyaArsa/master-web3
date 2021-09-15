using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Warehousing.Web.Application.Queries
{
    public static class QueriesDependenciesRegister
    {
        public static void AddQueriesDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("WarehousingConnection");

            serviceCollection.AddScoped(c => new WarehouseQueries(connString));
            serviceCollection.AddScoped(c => new ParcelQueries(connString));
            serviceCollection.AddScoped(c => new ContractQueries(connString));
            serviceCollection.AddScoped(c => new ParcelTypeQueries(connString));
            serviceCollection.AddScoped(c => new SupplierQueries(connString));
        }
    }
}
