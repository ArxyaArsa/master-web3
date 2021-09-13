using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.AggregateModels.SupplierAggregate;
using Warehousing.Domain.AggregateModels.WarehouseLotAggregate;

namespace Warehousing.Infrastructure.Repositories
{
    public static class RepositoriesDependenciesRegister
    {
        public static void AddRepositoriesDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IWarehouseLotRepository), typeof(WarehouseLotRepository));
            serviceCollection.AddScoped(typeof(ISupplierRepository), typeof(SupplierRepository));
        }
    }
}
