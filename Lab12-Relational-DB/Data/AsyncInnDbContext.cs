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
            //ModelBuilder.Entity<Hotel>().HasData(
            //    new Student
            //    {
            //        Id = 1,
            //        FirstName = "Jane",
            //        LastName = "Shepard",
            //        Birthdate = new DateTime(1970, 3, 5)
            //    },
            //    new Student
            //    {
            //        Id = 2,
            //        FirstName = "Kate",
            //        LastName = "Austin",
            //        Birthdate = new DateTime(1980, 11, 11)
            //    }
            //    );

            //ModelBuilder.Entity<Room>()HasData(
            //    new Room
            //    {
            //        ID = 1, 
            //        Price  = 100m,
            //    }
            //    new Room
            //    {
            //        ID = 1,
            //        Price = 100m,
            //    }
            //    );
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
