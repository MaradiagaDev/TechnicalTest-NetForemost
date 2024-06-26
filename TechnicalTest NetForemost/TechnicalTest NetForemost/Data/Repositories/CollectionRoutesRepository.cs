using System.Data.SqlClient;
using System.Data;
using TechnicalTest_NetForemost.Models;
using TechnicalTest_NetForemost.Data.Repositories.TechnicalTest_NetForemost.Data;

namespace TechnicalTest_NetForemost.Data.Repositories
{
    public class CollectionRoutesRepository : ICollectionRoutesRepository
    {
        private readonly DatabaseHelper _databaseHelper = new DatabaseHelper(AppSettings.Configuration.GetConnectionString("DefaultConnection"));

        public List<CollectionRoutes> GetSumByCollector()
        {
            List<CollectionRoutes> collectionRoutes = new List<CollectionRoutes>();

            using (var command = _databaseHelper.CreateCommand("financial.sp_AssignBalancesToCollectors"))
            {
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    _databaseHelper.OpenConnection();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CollectionRoutes obj = new CollectionRoutes()
                            {
                                debtCollectorID = Convert.ToInt32(reader[0]),
                                debtCollector = reader[1].ToString(),
                                NumberRoutes = int.Parse(reader[2].ToString()),
                                AmountOwed = decimal.Parse(reader[3].ToString()),
                            };

                            collectionRoutes.Add(obj);
                        }

                        return collectionRoutes;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error getting Collection Routes.", ex);
                }
                finally
                {
                    _databaseHelper.CloseConnection();
                }
            }
        }
    }
}
