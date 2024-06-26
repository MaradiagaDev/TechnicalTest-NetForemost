using System.Data.SqlClient;
using TechnicalTest_NetForemost.Data.Repositories.TechnicalTest_NetForemost.Data;
using TechnicalTest_NetForemost.DTO;

namespace TechnicalTest_NetForemost.Data.Repositories
{
    public class AccountsReceivableRepository : IAccountsReceivableRepository
    {
        private readonly DatabaseHelper _databaseHelper = new DatabaseHelper(AppSettings.Configuration.GetConnectionString("DefaultConnection"));
        public string CreateObject(AccountsReceivableDto dto)
        {
            using (var command = _databaseHelper.CreateCommand("Customers.sp_CreateAccountsReceivable"))
            {
                command.Parameters.Add(new SqlParameter("@CustomerID", dto.customerID));
                command.Parameters.Add(new SqlParameter("@InvoiceID", dto.invoiceID));
                command.Parameters.Add(new SqlParameter("@AmountOwed", dto.amountOwed));
                command.Parameters.Add(new SqlParameter("@DueDate", dto.dueDate));

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
