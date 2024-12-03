using scbH60Services.Models;
namespace scbH60Services.Interfaces
{
    public interface ICartItemService
    {
        Task<CartItem> GetCartItemById(int cartItemId);
        Task AddItemToCart(int cartId, int productId, int quantity);
        Task UpdateCartItemQuantity(int cartItemId, int newQuantity);
        Task RemoveItemFromCart(int cartItemId);
        Task<List<CartItem>> GetCartItemsByCartId(int cartId);
    }
}
