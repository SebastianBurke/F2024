using Microsoft.EntityFrameworkCore;
using scbH60Services.DAL;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Add DbContext for Products and ProductCategories
builder.Services.AddDbContext<H60AssignmentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

// Register your ProductService and other services
builder.Services.AddScoped<IProductService, ProductService>(); // Add ProductService
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>(); // Add ProductCategoryService if necessary
builder.Services.AddScoped<IGlobalSettingsService, GlobalSettingsService>(); // If you're using a settings service

// Add controllers
builder.Services.AddControllers();

// CORS setup (optional, but useful if calling from another domain)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

// Swagger/OpenAPI setup for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.UseCors("AllowAll"); // Apply CORS policy

app.UseAuthorization();

app.MapControllers();

app.Run();
