using Microsoft.EntityFrameworkCore;
using scbH60Services.Interfaces;
using scbH60Services.Models;

namespace scbH60Services.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly H60AssignmentDbContext _context;

        public CartItemService(H60AssignmentDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem> GetCartItemById(int cartItemId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId);
        }

        public async Task AddItemToCart(int cartId, int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null || product.Stock < quantity)
            {
                throw new InvalidOperationException("Not enough stock available.");
            }

            var cartItem = new CartItem
            {
                CartId = cartId,
                ProductId = productId,
                Quantity = quantity,
                Price = product.SellPrice.GetValueOrDefault()
            };

            product.Stock -= quantity;

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemQuantity(int cartItemId, int newQuantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null) throw new KeyNotFoundException("Cart item not found.");

            var product = await _context.Products.FindAsync(cartItem.ProductId);
            if (product == null) throw new InvalidOperationException("Associated product not found.");

            // Update stock
            var stockDifference = newQuantity - cartItem.Quantity;
            if (product.Stock < stockDifference)
            {
                throw new InvalidOperationException("Not enough stock to update the quantity.");
            }

            cartItem.Quantity = newQuantity;
            product.Stock -= stockDifference;

            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemFromCart(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                var product = await _context.Products.FindAsync(cartItem.ProductId);
                if (product != null)
                {
                    product.Stock += cartItem.Quantity;
                }

                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CartItem>> GetCartItemsByCartId(int cartId)
        {
            return await _context.CartItems
                .Where(ci => ci.CartId == cartId)
                .Include(ci => ci.Cart)
                .ToListAsync();
        }
    }

}
