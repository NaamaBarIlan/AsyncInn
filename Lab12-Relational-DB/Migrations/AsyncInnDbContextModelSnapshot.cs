﻿// <auto-generated />
using Lab12_Relational_DB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lab12_Relational_DB.Migrations
{
    [DbContext(typeof(AsyncInnDbContext))]
    partial class AsyncInnDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lab12_Relational_DB.Model.Amenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Amenities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "TV"
                        },
                        new
                        {
                            Id = 2,
                            Name = "AC"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Mini bar"
                        });
                });

            modelBuilder.Entity("Lab12_Relational_DB.Model.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Denver",
                            Name = "The Ballard Async Hotel",
                            Phone = "(303) 389-3000",
                            State = "CO",
                            StreetAddress = "1234, Main St"
                        },
                        new
                        {
                            Id = 2,
                            City = "Austin",
                            Name = "The Green Lake Async Hotel",
                            Phone = "(512) 482-8000",
                            State = "TX",
                            StreetAddress = "1234, Main St"
                        },
                        new
                        {
                            Id = 3,
                            City = "New York",
                            Name = "The Wallingford Async Hotel",
                            Phone = "(212) 389-3000",
                            State = "NY",
                            StreetAddress = "1234, Main St"
                        });
                });

            modelBuilder.Entity("Lab12_Relational_DB.Model.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Layout")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Layout = 0,
                            Name = "Seattle Snooze"
                        },
                        new
                        {
                            Id = 2,
                            Layout = 1,
                            Name = "Fremont Fun"
                        },
                        new
                        {
                            Id = 3,
                            Layout = 2,
                            Name = "Georgetown Glow"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
