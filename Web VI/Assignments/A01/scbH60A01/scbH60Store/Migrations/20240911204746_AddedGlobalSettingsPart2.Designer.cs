﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using scbH60Store.Models;

#nullable disable

namespace scbH60Store.Migrations
{
    [DbContext(typeof(H60AssignmentDbContext))]
    [Migration("20240911204746_AddedGlobalSettingsPart2")]
    partial class AddedGlobalSettingsPart2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("scbH60Store.Models.GlobalSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MaxStockLimit")
                        .HasColumnType("int");

                    b.Property<int>("MinStockLimit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GlobalSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MaxStockLimit = 100,
                            MinStockLimit = 10
                        });
                });

            modelBuilder.Entity("scbH60Store.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<decimal?>("BuyPrice")
                        .HasColumnType("numeric(8, 2)");

                    b.Property<string>("Description")
                        .HasMaxLength(80)
                        .IsUnicode(false)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("EmployeeNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer")
                        .HasMaxLength(80)
                        .IsUnicode(false)
                        .HasColumnType("varchar(80)");

                    b.Property<int>("ProdCatId")
                        .HasColumnType("int");

                    b.Property<decimal?>("SellPrice")
                        .HasColumnType("numeric(8, 2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex(new[] { "ProdCatId" }, "IX_Product_ProdCatId");

                    b.ToTable("Product", (string)null);

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            BuyPrice = 10.00m,
                            Description = "Spider-Man: Homecoming",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Marvel",
                            ProdCatId = 1,
                            SellPrice = 15.00m,
                            Stock = 100
                        },
                        new
                        {
                            ProductId = 2,
                            BuyPrice = 8.00m,
                            Description = "Batman: The Killing Joke",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "DC",
                            ProdCatId = 1,
                            SellPrice = 12.00m,
                            Stock = 50
                        },
                        new
                        {
                            ProductId = 3,
                            BuyPrice = 9.00m,
                            Description = "Wonder Woman: Blood",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "DC",
                            ProdCatId = 1,
                            SellPrice = 13.00m,
                            Stock = 70
                        },
                        new
                        {
                            ProductId = 4,
                            BuyPrice = 12.00m,
                            Description = "The Avengers: Endgame",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Marvel",
                            ProdCatId = 1,
                            SellPrice = 18.00m,
                            Stock = 80
                        },
                        new
                        {
                            ProductId = 5,
                            BuyPrice = 5.00m,
                            Description = "Naruto: Volume 1",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Shonen Jump",
                            ProdCatId = 2,
                            SellPrice = 7.00m,
                            Stock = 120
                        },
                        new
                        {
                            ProductId = 6,
                            BuyPrice = 6.00m,
                            Description = "Attack on Titan: Volume 1",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Kodansha",
                            ProdCatId = 2,
                            SellPrice = 8.00m,
                            Stock = 90
                        },
                        new
                        {
                            ProductId = 7,
                            BuyPrice = 5.50m,
                            Description = "Dragon Ball Z: Volume 1",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Viz Media",
                            ProdCatId = 2,
                            SellPrice = 8.50m,
                            Stock = 110
                        },
                        new
                        {
                            ProductId = 8,
                            BuyPrice = 6.00m,
                            Description = "One Piece: Volume 1",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Shonen Jump",
                            ProdCatId = 2,
                            SellPrice = 9.00m,
                            Stock = 130
                        },
                        new
                        {
                            ProductId = 9,
                            BuyPrice = 15.00m,
                            Description = "Maus",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Pantheon",
                            ProdCatId = 3,
                            SellPrice = 20.00m,
                            Stock = 60
                        },
                        new
                        {
                            ProductId = 10,
                            BuyPrice = 18.00m,
                            Description = "Watchmen",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "DC",
                            ProdCatId = 3,
                            SellPrice = 25.00m,
                            Stock = 40
                        },
                        new
                        {
                            ProductId = 11,
                            BuyPrice = 12.00m,
                            Description = "Persepolis",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Pantheon",
                            ProdCatId = 3,
                            SellPrice = 17.00m,
                            Stock = 30
                        },
                        new
                        {
                            ProductId = 12,
                            BuyPrice = 20.00m,
                            Description = "Sandman",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Vertigo",
                            ProdCatId = 3,
                            SellPrice = 30.00m,
                            Stock = 50
                        },
                        new
                        {
                            ProductId = 13,
                            BuyPrice = 10.00m,
                            Description = "Saga",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Image Comics",
                            ProdCatId = 4,
                            SellPrice = 14.00m,
                            Stock = 70
                        },
                        new
                        {
                            ProductId = 14,
                            BuyPrice = 9.00m,
                            Description = "The Walking Dead",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Image Comics",
                            ProdCatId = 4,
                            SellPrice = 13.00m,
                            Stock = 60
                        },
                        new
                        {
                            ProductId = 15,
                            BuyPrice = 11.00m,
                            Description = "Black Hammer",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Dark Horse",
                            ProdCatId = 4,
                            SellPrice = 16.00m,
                            Stock = 40
                        },
                        new
                        {
                            ProductId = 16,
                            BuyPrice = 13.00m,
                            Description = "Y: The Last Man",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Vertigo",
                            ProdCatId = 4,
                            SellPrice = 18.00m,
                            Stock = 50
                        },
                        new
                        {
                            ProductId = 17,
                            BuyPrice = 5.00m,
                            Description = "Dog Man",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Scholastic",
                            ProdCatId = 5,
                            SellPrice = 8.00m,
                            Stock = 80
                        },
                        new
                        {
                            ProductId = 18,
                            BuyPrice = 7.00m,
                            Description = "The Adventures of Tintin",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Little, Brown",
                            ProdCatId = 5,
                            SellPrice = 10.00m,
                            Stock = 70
                        },
                        new
                        {
                            ProductId = 19,
                            BuyPrice = 6.00m,
                            Description = "Bone",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Graphix",
                            ProdCatId = 5,
                            SellPrice = 9.00m,
                            Stock = 90
                        },
                        new
                        {
                            ProductId = 20,
                            BuyPrice = 8.00m,
                            Description = "Amulet",
                            EmployeeNotes = "In demand",
                            ImageUrl = "image.jpg",
                            Manufacturer = "Scholastic",
                            ProdCatId = 5,
                            SellPrice = 12.00m,
                            Stock = 100
                        });
                });

            modelBuilder.Entity("scbH60Store.Models.ProductCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProdCat")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)");

                    b.HasKey("CategoryId");

                    b.ToTable("ProductCategory", (string)null);

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            ImageUrl = "cat_superhero.jpg",
                            ProdCat = "Superhero"
                        },
                        new
                        {
                            CategoryId = 2,
                            ImageUrl = "cat_manga.jpg",
                            ProdCat = "Manga"
                        },
                        new
                        {
                            CategoryId = 3,
                            ImageUrl = "cat_graphic_novels.jpg",
                            ProdCat = "Graphic Novels"
                        },
                        new
                        {
                            CategoryId = 4,
                            ImageUrl = "cat_indie_comics.jpg",
                            ProdCat = "Indie Comics"
                        },
                        new
                        {
                            CategoryId = 5,
                            ImageUrl = "cat_kids_comics.jpg",
                            ProdCat = "Kids' Comics"
                        });
                });

            modelBuilder.Entity("scbH60Store.Models.Product", b =>
                {
                    b.HasOne("scbH60Store.Models.ProductCategory", "ProdCat")
                        .WithMany("Products")
                        .HasForeignKey("ProdCatId")
                        .IsRequired()
                        .HasConstraintName("FK_Product_ProductCategory");

                    b.Navigation("ProdCat");
                });

            modelBuilder.Entity("scbH60Store.Models.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
