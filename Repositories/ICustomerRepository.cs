using BusinessObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int id);
        Task<Customer> GetCustomerByEmail(string email);
        Task<IEnumerable<Customer>> GetCustomersByType(int type);
        Task<Customer> ValidateUser(string email, string password);
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(int customerId);
        Task<Customer> GetCustomerByEmailAndPassword(string email, string password);
    }
}
