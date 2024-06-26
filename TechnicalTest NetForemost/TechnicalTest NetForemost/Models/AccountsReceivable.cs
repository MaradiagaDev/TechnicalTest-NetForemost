using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.VisualBasic;

namespace TechnicalTest_NetForemost.Models
{
    public class AccountsReceivable
    {
       public int accountID {get; set;}
       public int customerID {get; set;}
       public int invoiceID {get; set;}
       decimal amountOwed {get; set;}
       public DateOnly dueDate {get; set;}
    }
}
