using System.Threading.Tasks;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.ParcelAggregate
{
    public interface IParcelRepository : IRepository<Parcel>
    {
        Parcel Add(Parcel p);
        void Update(Parcel p);
        Task<Parcel> GetAsync(int pId);
    }
}
