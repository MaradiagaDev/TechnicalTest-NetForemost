using TechnicalTest_NetForemost.Models;

namespace TechnicalTest_NetForemost.Data.Repositories
{
    public interface IAuxRepository<T>
    {
        public string CreateObject(string firstName, string lastName);
        public string UpdateObject(T obj);
        public T GetObjectById(int ID);
        public List<T> GetAllObjects(int offSet, int pageSize);
    }
}
