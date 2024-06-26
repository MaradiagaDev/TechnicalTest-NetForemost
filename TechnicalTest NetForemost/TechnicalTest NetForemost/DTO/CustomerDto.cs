using TechnicalTest_NetForemost.Data.Repositories;
using TechnicalTest_NetForemost.Models;

namespace TechnicalTest_NetForemost.DTO
{
    public class CustomerDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        private readonly CustomerRepository _repository = new CustomerRepository();

        public string CreateCustomer()
        {
            string message = _repository.CreateObject(firstName, lastName);
            if (message == "")
            {
                return "";
            }
            else { return message; }
        }

        public string UpdateCustomer(int id)
        {
            Customer customer = new Customer()
            {
                customerID = id,
                firstName = firstName,
                lastName = lastName,
            };

            string message = _repository.UpdateObject(customer);
            if (message == "")
            {
                return "";
            }
            else { return message; }
        }

        public Customer GetByIdCustomer(int id) => _repository.GetObjectById(id);

        public List<Customer> GetAllCustomer(int offSet, int pageSize) => _repository.GetAllObjects(offSet, pageSize);

    }
}
