using BusinessObject;
using DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await CustomerDAO.Instance.GetCustomerAll();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await CustomerDAO.Instance.GetCustomerById(id);
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await CustomerDAO.Instance.GetCustomerByEmail(email);
        }

        public async Task<IEnumerable<Customer>> GetCustomersByType(int type)
        {
            return await CustomerDAO.Instance.GetCustomersByType(type);
        }

        public async Task<Customer> ValidateUser(string email, string password)
        {
            return await CustomerDAO.Instance.ValidateUser(email, password);
        }

        public async Task AddCustomer(Customer customer)
        {
            await CustomerDAO.Instance.Add(customer);
        }

        public async Task UpdateCustomer(Customer customer)
        {
            await CustomerDAO.Instance.Update(customer);
        }

        public async Task DeleteCustomer(int id)
        {
            await CustomerDAO.Instance.Delete(id);
        }

        public async Task<Customer> GetCustomerByEmailAndPassword(string email, string password)
        {
            return await CustomerDAO.Instance.GetCustomerByEmailAndPassword(email, password);
        }
    }
}
