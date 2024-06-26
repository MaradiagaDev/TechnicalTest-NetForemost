using TechnicalTest_NetForemost.Data.Repositories;
using TechnicalTest_NetForemost.Models;

namespace TechnicalTest_NetForemost.DTO
{
    public class CollectionRoutesDto
    {
        CollectionRoutesRepository _repository = new CollectionRoutesRepository();

        public List<CollectionRoutes> GetSumByCollector()
        {
            return _repository.GetSumByCollector();
        }
    }
}
