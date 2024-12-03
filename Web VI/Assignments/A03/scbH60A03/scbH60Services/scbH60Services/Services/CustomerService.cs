using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using scbH60Services.Interfaces;
using scbH60Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scbH60Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly H60AssignmentDbContext _context;
        private const string CustomerRoleId = "da704f05-3645-43e5-b9a5-32f5509bef28";

        public CustomerService(H60AssignmentDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllCustomers()
        {
            return await _context.Users
                .Where(u => _context.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == CustomerRoleId))
                .OrderBy(u => u.LastName) // Ensure LastName and FirstName are properties of User
                .ThenBy(u => u.FirstName)
                .ToListAsync();
        }

        public async Task<User> GetCustomerById(string id)
        {
            return await _context.Users
                .Where(u => u.Id == id && _context.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == CustomerRoleId))
                .FirstOrDefaultAsync();
        }

        public async Task<string> AddCustomer(User customer)
        {
            // Assign customer role
            _context.UserRoles.Add(new IdentityUserRole<string> { RoleId = CustomerRoleId, UserId = customer.Id });
            _context.Users.Add(customer);
            await _context.SaveChangesAsync();
            return "Customer added successfully!";
        }

        public async Task<string> UpdateCustomer(User customer)
        {
            var existingCustomer = await GetCustomerById(customer.Id);
            if (existingCustomer == null)
            {
                return "Customer not found.";
            }

            // Update relevant fields
            existingCustomer.UserName = customer.UserName;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;

            _context.Users.Update(existingCustomer);
            await _context.SaveChangesAsync();
            return "Customer updated successfully!";
        }

        public async Task<string> DeleteCustomer(string id)
        {
            // Retrieve the customer by ID
            var customer = await GetCustomerById(id);
            if (customer == null) return "Customer not found.";

            // Check if the customer has orders or a shopping cart
            var hasOrdersOrCart = _context.Orders.Any(o => o.CustomerId == id) ||
                                  _context.ShoppingCarts.Any(sc => sc.CustomerId == id);

            if (hasOrdersOrCart) return "Cannot delete customer with existing orders or cart.";

            // Delete the customer
            _context.Users.Remove(customer);
            await _context.SaveChangesAsync();
            return "Customer deleted successfully!";
        }


    }
}
