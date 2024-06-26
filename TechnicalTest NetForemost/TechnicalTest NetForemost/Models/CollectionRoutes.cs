using System.Security.Cryptography.Xml;

namespace TechnicalTest_NetForemost.Models
{
    public class CollectionRoutes
    {
      public int debtCollectorID { get; set;}
      public string debtCollector {get; set;}
      public int NumberRoutes { get; set;}
      public decimal AmountOwed { get; set;}
    }
}
