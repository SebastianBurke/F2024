using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using scbH60Store.Models;
using scbH60Store.DAL;
using scbH60Services.Models;
using scbH60Services.Interfaces;
using scbH60Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services for customer views
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<H60AssignmentDbContext>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IProductAPIService, ProductAPIService>();
builder.Services.AddScoped<IProductCategoryAPIService, ProductCategoryAPIService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<IProductValidator, ProductValidator>();
builder.Services.AddHttpClient<IProductQueryService, ProductQueryService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:21905/api/");
});

// Identity setup
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<H60AssignmentDbContext>()
    .AddDefaultTokenProviders();

// Identity options configuration
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;

    options.User.RequireUniqueEmail = true;
});

// Authorization services with Customer role restriction
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomerOnly", policy => policy.RequireRole("Customer"));
});

var app = builder.Build();

// Configure middleware pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:21905")
          .AllowAnyHeader()
          .AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CustomerAccount}/{action=Login}/{id?}");

// Create roles on startup, focusing on "Customer" role for customers
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();}

app.Run();
