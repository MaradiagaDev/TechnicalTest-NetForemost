using TechnicalTest_NetForemost.Data.Repositories;
using TechnicalTest_NetForemost.Models;

namespace TechnicalTest_NetForemost.DTO
{
    public partial class DebtCollectorDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        private readonly DebtCollectorRepository _repository = new DebtCollectorRepository();

        public string CreateDebtCollector()
        {
            string message = _repository.CreateObject(firstName, lastName);
            if ( message == "")
            {
                return "";
            }
            else { return message; }
        }

        public string UpdateDebtCollector(int id) 
        {
            DebtCollector debtCollector = new DebtCollector()
            {
                debtCollectorID = id,
                firstName = firstName,
                lastName = lastName,
            };

            string message = _repository.UpdateObject(debtCollector);
            if (message == "")
            {
                return "";
            }
            else { return message; }
        }

        public DebtCollector GetByIdDebtCollectorDto(int id) => _repository.GetObjectById(id);

        public List<DebtCollector> GetAllDebtCollectorPage(int offSet, int pageSize) => _repository.GetAllObjects(offSet,pageSize);

    }
}
