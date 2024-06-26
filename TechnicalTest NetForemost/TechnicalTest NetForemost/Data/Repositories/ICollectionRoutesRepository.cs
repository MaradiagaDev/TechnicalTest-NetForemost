using TechnicalTest_NetForemost.DTO;
using TechnicalTest_NetForemost.Models;

namespace TechnicalTest_NetForemost.Data.Repositories
{
    public interface ICollectionRoutesRepository
    {
        public List<CollectionRoutes> GetSumByCollector();
    }
}
