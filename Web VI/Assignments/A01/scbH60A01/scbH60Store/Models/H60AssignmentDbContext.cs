using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace scbH60Store.Models;

public partial class H60AssignmentDbContext : DbContext
{
    public H60AssignmentDbContext()
    {
    }

    public H60AssignmentDbContext(DbContextOptions<H60AssignmentDbContext> options)
        : base(options)
    {
        //Database.EnsureCreated();  // this has to be commented out for migrations to be run
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<GlobalSettings> GlobalSettings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=cssql.cegep-heritage.qc.ca;Database=H60AssignmentDB_scb;User Id=SCANALESBURKE;Password=password;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.ProdCatId, "IX_Product_ProdCatId");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.BuyPrice).HasColumnType("numeric(8, 2)");
            entity.Property(e => e.Description)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.SellPrice).HasColumnType("numeric(8, 2)");

            entity.HasOne(d => d.ProdCat).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProdCatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_ProductCategory");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("ProductCategory");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ProdCat)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        // Seed data
        modelBuilder.Entity<ProductCategory>().HasData(
            new ProductCategory { CategoryId = 1, ProdCat = "Superhero", ImageUrl = "cat_superhero.jpg" },
            new ProductCategory { CategoryId = 2, ProdCat = "Manga", ImageUrl = "cat_manga.jpg" },
            new ProductCategory { CategoryId = 3, ProdCat = "Graphic Novels", ImageUrl = "cat_graphic_novels.jpg" },
            new ProductCategory { CategoryId = 4, ProdCat = "Indie Comics", ImageUrl = "cat_indie_comics.jpg" },
            new ProductCategory { CategoryId = 5, ProdCat = "Kids' Comics", ImageUrl = "cat_kids_comics.jpg" }
        );

        modelBuilder.Entity<Product>().HasData(
            // Superhero Category Products
            new Product { ProductId = 1, ProdCatId = 1, Description = "Spider-Man: Homecoming", Manufacturer = "Marvel", Stock = 100, BuyPrice = 10.00m, SellPrice = 15.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 2, ProdCatId = 1, Description = "Batman: The Killing Joke", Manufacturer = "DC", Stock = 50, BuyPrice = 8.00m, SellPrice = 12.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 3, ProdCatId = 1, Description = "Wonder Woman: Blood", Manufacturer = "DC", Stock = 70, BuyPrice = 9.00m, SellPrice = 13.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 4, ProdCatId = 1, Description = "The Avengers: Endgame", Manufacturer = "Marvel", Stock = 80, BuyPrice = 12.00m, SellPrice = 18.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },

            // Manga Category Products
            new Product { ProductId = 5, ProdCatId = 2, Description = "Naruto: Volume 1", Manufacturer = "Shonen Jump", Stock = 120, BuyPrice = 5.00m, SellPrice = 7.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 6, ProdCatId = 2, Description = "Attack on Titan: Volume 1", Manufacturer = "Kodansha", Stock = 90, BuyPrice = 6.00m, SellPrice = 8.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 7, ProdCatId = 2, Description = "Dragon Ball Z: Volume 1", Manufacturer = "Viz Media", Stock = 110, BuyPrice = 5.50m, SellPrice = 8.50m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 8, ProdCatId = 2, Description = "One Piece: Volume 1", Manufacturer = "Shonen Jump", Stock = 130, BuyPrice = 6.00m, SellPrice = 9.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },

            // Graphic Novels Category Products
            new Product { ProductId = 9, ProdCatId = 3, Description = "Maus", Manufacturer = "Pantheon", Stock = 60, BuyPrice = 15.00m, SellPrice = 20.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 10, ProdCatId = 3, Description = "Watchmen", Manufacturer = "DC", Stock = 40, BuyPrice = 18.00m, SellPrice = 25.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 11, ProdCatId = 3, Description = "Persepolis", Manufacturer = "Pantheon", Stock = 30, BuyPrice = 12.00m, SellPrice = 17.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 12, ProdCatId = 3, Description = "Sandman", Manufacturer = "Vertigo", Stock = 50, BuyPrice = 20.00m, SellPrice = 30.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },

            // Indie Comics Category Products
            new Product { ProductId = 13, ProdCatId = 4, Description = "Saga", Manufacturer = "Image Comics", Stock = 70, BuyPrice = 10.00m, SellPrice = 14.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg"  },
            new Product { ProductId = 14, ProdCatId = 4, Description = "The Walking Dead", Manufacturer = "Image Comics", Stock = 60, BuyPrice = 9.00m, SellPrice = 13.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 15, ProdCatId = 4, Description = "Black Hammer", Manufacturer = "Dark Horse", Stock = 40, BuyPrice = 11.00m, SellPrice = 16.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 16, ProdCatId = 4, Description = "Y: The Last Man", Manufacturer = "Vertigo", Stock = 50, BuyPrice = 13.00m, SellPrice = 18.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },

            // Kids' Comics Category Products
            new Product { ProductId = 17, ProdCatId = 5, Description = "Dog Man", Manufacturer = "Scholastic", Stock = 80, BuyPrice = 5.00m, SellPrice = 8.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 18, ProdCatId = 5, Description = "The Adventures of Tintin", Manufacturer = "Little, Brown", Stock = 70, BuyPrice = 7.00m, SellPrice = 10.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 19, ProdCatId = 5, Description = "Bone", Manufacturer = "Graphix", Stock = 90, BuyPrice = 6.00m, SellPrice = 9.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" },
            new Product { ProductId = 20, ProdCatId = 5, Description = "Amulet", Manufacturer = "Scholastic", Stock = 100, BuyPrice = 8.00m, SellPrice = 12.00m, EmployeeNotes = "In demand", ImageUrl = "image.jpg" }
        );

        // Seed default global settings
        modelBuilder.Entity<GlobalSettings>().HasData(
            new GlobalSettings { Id = 1, MinStockLimit = 10, MaxStockLimit = 100 }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
