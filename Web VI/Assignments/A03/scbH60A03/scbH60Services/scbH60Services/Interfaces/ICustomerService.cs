using System.Collections.Generic;
using System.Threading.Tasks;
using scbH60Services.Models; // Ensure this is the namespace where your custom User model is located

namespace scbH60Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<User>> GetAllCustomers(); // Replace User with your custom user model if different
        Task<User> GetCustomerById(string id);
        Task<string> AddCustomer(User customer);
        Task<string> UpdateCustomer(User customer);
        Task<string> DeleteCustomer(string id);
    }
}
