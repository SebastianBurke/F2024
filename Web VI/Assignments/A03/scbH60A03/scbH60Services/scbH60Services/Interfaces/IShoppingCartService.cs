using scbH60Services.Models;
namespace scbH60Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetCartById(int cartId);
        Task<ShoppingCart> CreateCart(string customerId);
        Task DeleteCart(int cartId);
        Task<List<CartItem>> GetItemsInCart(int cartId);
        Task<ShoppingCart> GetCartByCustomerId(string customerId);
    }
}
