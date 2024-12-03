using Xunit;
using Microsoft.EntityFrameworkCore;
using scbH60Services.Models;
using scbH60Services.Services;

public class CartItemServiceTests
{
    private DbContextOptions<H60AssignmentDbContext> GetDbOptions()
    {
        return new DbContextOptionsBuilder<H60AssignmentDbContext>()
            .UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}") // Unique database
            .EnableSensitiveDataLogging()
            .Options;
    }

    [Fact]
    public async Task AddItemToCart_ShouldThrowException_WhenStockIsInsufficient()
    {
        // Arrange
        var options = GetDbOptions();
        using var context = new H60AssignmentDbContext(options);
        var product = new Product
        {
            ProductId = 1,
            Stock = 0,
            Description = "Test Product",
            SellPrice = 10.0M
        };
        context.Products.Add(product);
        await context.SaveChangesAsync();


        var service = new CartItemService(context);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddItemToCart(1, 1, 5));
        Assert.Equal("Not enough stock available.", exception.Message);
    }

    [Fact]
    public async Task UpdateCartItemQuantity_ShouldThrowException_WhenStockIsInsufficient()
    {
        // Arrange
        var options = GetDbOptions();
        using var context = new H60AssignmentDbContext(options);

        var product = new Product
        {
            ProductId = 1,
            Stock = 5,
            Description = "Test Product",
            SellPrice = 10.0M
        };
        context.Products.Add(product);

        var cartItem = new CartItem
        {
            CartItemId = 1,
            ProductId = 1,
            Quantity = 3,
            Price = 10.0M
        };
        context.CartItems.Add(cartItem);

        await context.SaveChangesAsync();

        var service = new CartItemService(context);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.UpdateCartItemQuantity(cartItem.CartItemId, 10));

        Assert.Equal("Not enough stock to update the quantity.", exception.Message);
    }

    [Fact]
    public async Task RemoveItemFromCart_ShouldIncreaseStock()
    {
        // Arrange
        var options = GetDbOptions();
        using var context = new H60AssignmentDbContext(options);

        var product = new Product
        {
            ProductId = 1,
            Stock = 10,
            Description = "Test Product", // Required field
            SellPrice = 10.0M
        };
        context.Products.Add(product);

        var cartItem = new CartItem
        {
            CartId = 1,
            ProductId = 1,
            Quantity = 2,
            Price = 10.0M
        };
        context.CartItems.Add(cartItem);

        await context.SaveChangesAsync();

        var service = new CartItemService(context);

        // Act
        await service.RemoveItemFromCart(cartItem.CartItemId);

        // Assert
        Assert.Null(await context.CartItems.FindAsync(cartItem.CartItemId));
        Assert.Equal(12, product.Stock);
    }

    [Fact]
    public async Task GetCartItemsByCartId_ShouldReturnItems()
    {
        // Arrange
        var options = GetDbOptions();
        using (var context = new H60AssignmentDbContext(options))
        {
            // Ensure a clean database state
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var cart = new ShoppingCart { ShoppingCartId = 1, CustomerId = "1" };
            context.ShoppingCarts.Add(cart);

            var cartItem = new CartItem
            {
                CartId = cart.ShoppingCartId, // Link to the correct cart
                ProductId = 1,
                Quantity = 2,
                Price = 10.0M
            };

            context.CartItems.Add(cartItem);

            await context.SaveChangesAsync();
        }

        using (var context = new H60AssignmentDbContext(options))
        {
            var service = new CartItemService(context);

            // Act
            var items = await service.GetCartItemsByCartId(1);

            // Assert
            Assert.NotNull(items);
            Assert.Single(items);
            Assert.Equal(2, items[0].Quantity);
            Assert.Equal(1, items[0].CartId);
        }
    }


}
