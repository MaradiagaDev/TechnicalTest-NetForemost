using System.Data;
using System.Data.SqlClient;
using TechnicalTest_NetForemost.Data.Repositories.TechnicalTest_NetForemost.Data;
using TechnicalTest_NetForemost.Models;

namespace TechnicalTest_NetForemost.Data.Repositories
{
    public class CustomerRepository : IAuxRepository<Customer>
    {
        private readonly DatabaseHelper _databaseHelper = new DatabaseHelper(AppSettings.Configuration.GetConnectionString("DefaultConnection"));
        public string CreateObject(string firstName, string lastName)
        {
            using (var command = _databaseHelper.CreateCommand("Customers.sp_CreateCustomer"))
            {
                command.Parameters.Add(new SqlParameter("@firstName", firstName));
                command.Parameters.Add(new SqlParameter("@lastName", lastName));

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

        public List<Customer> GetAllObjects(int offSet, int pageSize)
        {
            List<Customer> customers = new List<Customer>();

            using (var command = _databaseHelper.CreateCommand("Customers.sp_GetAllCustomersPage"))
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
                            Customer obj = new Customer()
                            {
                                customerID = Convert.ToInt32(reader["CustomerID"]),
                                firstName = reader["FirstName"].ToString(),
                                lastName = reader["LastName"].ToString(),
                            };

                            customers.Add(obj);
                        }

                        return customers;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error getting paginated Customers.", ex);
                }
                finally
                {
                    _databaseHelper.CloseConnection();
                }
            }
        }

        public Customer GetObjectById(int ID)
        {
            using (var command = _databaseHelper.CreateCommand("Customers.sp_GetOneByOneCustomer"))
            {
                command.Parameters.Add(new SqlParameter("@CustomerID", ID));

                try
                {
                    _databaseHelper.OpenConnection();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Customer obj = new Customer
                            {
                                // Mapea los campos del lector al objeto DebtCollector
                                customerID = Convert.ToInt32(reader["CustomerID"]),
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
                    throw new Exception("Error getting Customer by ID", ex);
                }
                finally
                {
                    _databaseHelper.CloseConnection();
                }
            }
        }

        public string UpdateObject(Customer obj)
        {
            using (var command = _databaseHelper.CreateCommand("Customers.sp_UpdateCustomer"))
            {
                command.Parameters.Add(new SqlParameter("@customerID", obj.customerID));
                command.Parameters.Add(new SqlParameter("@firstName", obj.firstName));
                command.Parameters.Add(new SqlParameter("@lastName", obj.lastName));

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
