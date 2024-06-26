using TechnicalTest_NetForemost.Data.Repositories;

namespace TechnicalTest_NetForemost.DTO
{
    public class AccountsReceivableDto
    {
        public int customerID { get; set; }
        public int invoiceID { get; set; }
        public decimal amountOwed { get; set; }
        public DateTime dueDate { get; set; }

        private AccountsReceivableRepository _repository = new AccountsReceivableRepository();

        public string CreateAccountsReceivable()
        {
            string message = _repository.CreateObject(this);
            if (message == "")
            {
                return "";
            }
            else { return message; }
        }
    }
}
