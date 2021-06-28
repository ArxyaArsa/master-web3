using System.Threading.Tasks;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.SupplierAggregate
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Supplier Add(Supplier s);
        void Update(Supplier s);
        Task<Supplier> GetAsync(int sId);
    }
}
