using Microsoft.AspNetCore.Mvc.Formatters;
using Lab12_Relational_DB.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Data
{
    public class AsyncInnDbContext : DbContext
    {
        // adding a constructor, so when we make an instance of the DB it will have the right options
        // we'll always leave this constructor empty. 
        public AsyncInnDbContext(DbContextOptions<AsyncInnDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "The Ballard Async Hotel",
                    StreetAddress = "1234, Main St",
                    City = "Denver",
                    State = "CO",
                    Phone = "(303) 389-3000"
                },
                new Hotel
                {
                    Id = 2,
                    Name = "The Green Lake Async Hotel",
                    StreetAddress = "1234, Main St",
                    City = "Austin",
                    State = "TX",
                    Phone = "(512) 482-8000"
                },
                new Hotel
                {
                    Id = 3,
                    Name = "The Wallingford Async Hotel",
                    StreetAddress = "1234, Main St",
                    City = "New York",
                    State = "NY",
                    Phone = "(212) 389-3000"
                }
                );

            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    Name = "Seattle Snooze",
                    Layout = 0
                },
                new Room
                {
                    Id = 2,
                    Name = "Fremont Fun",
                    Layout = 1
                },
                new Room
                {
                    Id = 3,
                    Name = "Georgetown Glow",
                    Layout = 2
                }
                );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 1,
                    Name = "TV"
                },
                new Amenity
                {
                    Id = 2,
                    Name = "AC"
                },
                new Amenity
                {
                    Id = 3,
                    Name = "Mini bar"
                }
                );

        }

        // to create an initial migration
        // Install-Package Microsoft.EntityFrameworkCore.Tools
        // add-migration {migrationName}
        // add-migration initial
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Amenity> Amenities { get; set; }

    }
}
