using Xunit;
using Microsoft.EntityFrameworkCore;
using scbH60Services.Models;
using scbH60Services.Services;

public class ShoppingCartServiceTests
{
    private DbContextOptions<H60AssignmentDbContext> GetDbOptions()
    {
        return new DbContextOptionsBuilder<H60AssignmentDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
    }

    [Fact]
    public async Task CreateCart_ShouldCreateNewCart()
    {
        // Arrange
        var options = GetDbOptions();
        using var context = new H60AssignmentDbContext(options);
        var service = new ShoppingCartService(context);

        // Act
        var cart = await service.CreateCart("1");

        // Assert
        Assert.NotNull(cart);
        Assert.Equal("1", cart.CustomerId);
    }

    [Fact]
    public async Task GetCartById_ShouldReturnNull_WhenCartDoesNotExist()
    {
        // Arrange
        var options = GetDbOptions();
        using var context = new H60AssignmentDbContext(options);
        var service = new ShoppingCartService(context);

        // Act
        var result = await service.GetCartById(999); // Non-existent ID

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteCart_ShouldThrowException_WhenCartHasItems()
    {
        // Arrange
        var options = GetDbOptions();
        using var context = new H60AssignmentDbContext(options);
        var cart = new ShoppingCart { CustomerId = "1", CartItems = new List<CartItem> { new CartItem { Quantity = 1 } } };
        context.ShoppingCarts.Add(cart);
        await context.SaveChangesAsync();

        var service = new ShoppingCartService(context);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteCart(cart.ShoppingCartId));
        Assert.Equal("Cannot delete a cart that has items.", exception.Message);
    }

    [Fact]
    public async Task GetItemsInCart_ShouldReturnEmptyList_WhenCartHasNoItems()
    {
        // Arrange
        var options = GetDbOptions();
        using (var context = new H60AssignmentDbContext(options))
        {
            // Ensure a clean database
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var cart = new ShoppingCart { CustomerId = "1" };
            context.ShoppingCarts.Add(cart);
            await context.SaveChangesAsync();
        }

        using (var context = new H60AssignmentDbContext(options))
        {
            var service = new ShoppingCartService(context);

            // Act
            var items = await service.GetItemsInCart(1);

            // Assert
            Assert.NotNull(items);
            Assert.Empty(items); // Expect no items in the cart
        }
    }

    [Fact]
    public async Task UpdateCart_ShouldUpdateCustomerId()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<H60AssignmentDbContext>()
            .UseInMemoryDatabase("TestDb_UpdateCart")
            .EnableSensitiveDataLogging()
            .Options;

        using (var context = new H60AssignmentDbContext(options))
        {
            // Create and save a shopping cart
            var cart = new ShoppingCart { ShoppingCartId = 1, CustomerId = "1", DateCreated = DateTime.UtcNow };
            context.ShoppingCarts.Add(cart);
            await context.SaveChangesAsync();
        }

        using (var context = new H60AssignmentDbContext(options))
        {
            var service = new ShoppingCartService(context);

            // Act
            var cart = await service.GetCartById(1);
            cart.CustomerId = "2";
            context.ShoppingCarts.Update(cart);
            await context.SaveChangesAsync(); 

            // Assert
            var updatedCart = await service.GetCartById(1);
            Assert.NotNull(updatedCart);
            Assert.Equal("2", updatedCart.CustomerId);
        }
    }




}
