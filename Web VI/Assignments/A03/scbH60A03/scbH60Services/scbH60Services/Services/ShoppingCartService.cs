using Microsoft.EntityFrameworkCore;
using scbH60Services.Interfaces;
using scbH60Services.Models;

namespace scbH60Services.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly H60AssignmentDbContext _context;

        public ShoppingCartService(H60AssignmentDbContext context)
        {
            _context = context;
        }

        public async Task<ShoppingCart> CreateCart(string customerId)
        {
            var cart = new ShoppingCart
            {
                CustomerId = customerId,
                DateCreated = DateTime.UtcNow
            };
            _context.ShoppingCarts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<ShoppingCart> GetCartById(int cartId)
        {
            return await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.ShoppingCartId == cartId);
        }
        public async Task<ShoppingCart> GetCartByCustomerId(string customerId)
        {
            return await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task DeleteCart(int cartId)
        {
            var cart = await GetCartById(cartId);
            if (cart != null && !cart.CartItems.Any())
            {
                _context.ShoppingCarts.Remove(cart);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Cannot delete a cart that has items.");
            }
        }

        public async Task<List<CartItem>> GetItemsInCart(int cartId)
        {
            var cart = await GetCartById(cartId);
            return cart?.CartItems ?? new List<CartItem>();
        }

    }

}
