using api.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace api
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Pet> Pets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlServer("Server=cssql.cegep-heritage.qc.ca;Database=H60T02_scb;User Id=SCANALESBURKE;Password=password;TrustServerCertificate=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = new DateTime(1990, 5, 23)
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    BirthDate = new DateTime(1985, 8, 15)
                },
                new Person
                {
                    Id = 3,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    BirthDate = new DateTime(2000, 12, 1)
                },
                new Person
                {
                    Id = 4,
                    FirstName = "Bob",
                    LastName = "Brown",
                    BirthDate = new DateTime(1978, 3, 9)
                }
            );

            modelBuilder.Entity<Pet>().HasData(
                new Pet { Id = 1, Name = "Buddy", Type = "Dog", OwnerId = 1 },
                new Pet { Id = 2, Name = "Whiskers", Type = "Cat", OwnerId = 2 }
            );
        }
    }

}
