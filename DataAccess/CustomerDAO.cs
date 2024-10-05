using BusinessObject;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CustomerDAO : SingletonBase<CustomerDAO>
    {
        public async Task<Customer> GetCustomerByEmailAndPassword(string email, string password)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomerAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            return customer;
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomersByType(int type)
        {
            return await _context.Customers.Where(a => a.Type == type).ToListAsync();
        }

        public async Task<Customer> ValidateUser(string email, string password)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
            return customer;
        }

        public async Task Add(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }


        public async Task Update(Customer customer)
        {
            var existingItem = await GetCustomerById(customer.CustomerId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var customer = await GetCustomerById(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
