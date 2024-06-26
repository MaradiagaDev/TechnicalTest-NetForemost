using System.Data;
using System.Data.SqlClient;
using TechnicalTest_NetForemost.Data.Repositories.TechnicalTest_NetForemost.Data;
using TechnicalTest_NetForemost.Models;

namespace TechnicalTest_NetForemost.Data.Repositories
{
    public class DebtCollectorRepository : IAuxRepository<DebtCollector>
    {
        private readonly DatabaseHelper _databaseHelper = new DatabaseHelper(AppSettings.Configuration.GetConnectionString("DefaultConnection"));

        public string CreateObject(string firstName, string lastName)
        {
            using (var command = _databaseHelper.CreateCommand("HumanResources.sp_CreateDebtCollector"))
            {
                command.Parameters.Add(new SqlParameter("@firstName", firstName));
                command.Parameters.Add(new SqlParameter("@lastName", lastName));

                try
                {
                    _databaseHelper.OpenConnection();

                    int rowsAffected = command.ExecuteNonQuery();
                    return "";
                }
                catch(SqlException ex)
                {
                    return ex.Message;
                    throw;
                }
                finally
                {
                    _databaseHelper.CloseConnection();
                }
            }
        }

        public List<DebtCollector> GetAllObjects(int offSet, int pageSize)
        {
            List<DebtCollector> debtCollectors = new List<DebtCollector>();

            using (var command = _databaseHelper.CreateCommand("HumanResources.sp_GetAllDebtCollectorPage"))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Offset", offSet));
                command.Parameters.Add(new SqlParameter("@PageSize", pageSize));

                try
                {
                    _databaseHelper.OpenConnection();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DebtCollector obj = new DebtCollector
                            {
                                // Mapea los campos del lector al objeto DebtCollector
                                debtCollectorID = Convert.ToInt32(reader["DebtCollectorID"]),
                                firstName = reader["FirstName"].ToString(),
                                lastName = reader["LastName"].ToString(),
                                // Otros campos según tu modelo DebtCollector
                            };

                            debtCollectors.Add(obj);
                        }

                        return debtCollectors;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error getting paginated DebtCollectors", ex);
                }
                finally
                {
                    _databaseHelper.CloseConnection();
                }
            }
        }


        public DebtCollector GetObjectById(int debtCollectorID)
        {
            using (var command = _databaseHelper.CreateCommand("HumanResources.sp_GetOneByOneDebtCollector"))
            {
                command.Parameters.Add(new SqlParameter("@DebtCollectorID", debtCollectorID));

                try
                {
                    _databaseHelper.OpenConnection();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DebtCollector obj = new DebtCollector
                            {
                                // Mapea los campos del lector al objeto DebtCollector
                                debtCollectorID = Convert.ToInt32(reader["DebtCollectorID"]),
                                firstName = reader["FirstName"].ToString(),
                                lastName = reader["LastName"].ToString(),
                                // Otros campos según tu modelo DebtCollector
                            };

                            return obj;
                        }
                        else
                        {
                            return null;
                        }
                    }

                }
                catch (SqlException ex)
                {
                    throw new Exception("Error getting DebtCollector by ID", ex);
                }
                finally
                {
                    _databaseHelper.CloseConnection();
                }
            }
        }

        public string UpdateObject(DebtCollector debtCollector)
        {
            using (var command = _databaseHelper.CreateCommand("HumanResources.sp_UpdateDebtCollector"))
            {
                command.Parameters.Add(new SqlParameter("@DebtCollectorID", debtCollector.debtCollectorID));
                command.Parameters.Add(new SqlParameter("@firstName", debtCollector.firstName));
                command.Parameters.Add(new SqlParameter("@lastName", debtCollector.lastName));

                try
                {
                    _databaseHelper.OpenConnection();

                    int rowsAffected = command.ExecuteNonQuery();
                    return "";
                }
                catch (SqlException ex)
                {
                    return ex.Message;
                    throw;
                }
                finally
                {
                    _databaseHelper.CloseConnection();
                }
            }
        }
    }
}
