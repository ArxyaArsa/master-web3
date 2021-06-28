using System.Threading.Tasks;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.WarehouseLotAggregate
{
    public interface IWarehouseLotRepository : IRepository<WarehouseLot>
    {
        WarehouseLot Add(WarehouseLot wl);
        void Update(WarehouseLot wl);
        Task<WarehouseLot> GetAsync(int wlId);
    }
}
