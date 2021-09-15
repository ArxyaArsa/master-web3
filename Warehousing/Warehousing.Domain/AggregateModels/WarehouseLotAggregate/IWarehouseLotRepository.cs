using System.Threading.Tasks;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.WarehouseLotAggregate
{
    public interface IWarehouseLotRepository : IRepository<WarehouseLot>
    {
        WarehouseLot Add(WarehouseLot wl);
        void Update(WarehouseLot wl);
        void Update(ParcelType pt);
        void Remove(WarehouseLot wl);
        Task<WarehouseLot> GetAsync(int wlId, bool includeParcels = false);
        Task<Parcel> GetParcelAsync(int pId, bool includeWarehouseLot = true, bool includeParcelType = true);
        Task<ParcelType> GetParcelTypeAsync(int ptId, bool includeParcels = true);
        ParcelType AddParcelType(string name, string desc, decimal minWeightOccupied, decimal maxWeight, bool isPerishable, int? freezerLifetime, int? dryLifetime);
    }
}
