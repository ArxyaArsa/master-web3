using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
