using Lab12_Relational_DB.Model;
using Lab12_Relational_DB.Model.Interfaces;
using Lab12_Relational_DB.Model.Services;
using System;
using Xunit;
using System.Threading.Tasks;
using Lab12_Relational_DB.Model.DTOs;
using System.Collections.Generic;

namespace XUnitTest_AsyncInn
{
    public class AmenityServiceTest : DatabaseTestBase
    {
        private IAmenity BuildRepository()
        {
            return new AmenityRepository(_db);
        }

        [Fact]
        public async Task CanSaveAndGetAmenity()
        {
            // Arrange


            var amenity = new Amenity
            {
                Id = 22,
                Name = "TV"
            };

            var amenityDto = new AmenityDTO
            {
                ID = amenity.Id,
                Name = amenity.Name
            };

            var repository = BuildRepository();

            // Act
            AmenityDTO saved = await repository.Create(amenityDto);

            // Assert

            Assert.NotNull(saved);
            Assert.NotEqual(0, amenityDto.ID);
            Assert.Equal(saved.ID, amenityDto.ID);
            Assert.Equal(amenity.Name, saved.Name);
        }

        [Fact]
        public async Task CanGetASingleAmenity()
        {
            // Arrange

            Amenity firstAmenity = new Amenity
            {
                Id = 11,
                Name = "TV"
            };

            AmenityDTO firstAmenityDto = new AmenityDTO
            {
                ID = firstAmenity.Id,
                Name = firstAmenity.Name
            };

            Amenity secondAmenity = new Amenity
            {
                Id = 12,
                Name = "AC"
            };

            AmenityDTO secondAmenityDto = new AmenityDTO
            {
                ID = secondAmenity.Id,
                Name = secondAmenity.Name
            };

            var repository = BuildRepository();

            AmenityDTO saved1 = await repository.Create(firstAmenityDto);
            AmenityDTO saved2 = await repository.Create(secondAmenityDto);

            // Act

            AmenityDTO result1 = await repository.GetAmenity(1);
            AmenityDTO result2 = await repository.GetAmenity(2);

            // Assert

            Assert.Equal("TV", result1.Name);
            Assert.Equal("AC", result2.Name);
        }

        [Fact]
        public async Task CanGetAllAmenities()
        {
            // Arrange

            Amenity firstAmenity = new Amenity
            {
                Id = 11,
                Name = "TV"
            };

            AmenityDTO firstAmenityDto = new AmenityDTO
            {
                ID = firstAmenity.Id,
                Name = firstAmenity.Name
            };

            Amenity secondAmenity = new Amenity
            {
                Id = 12,
                Name = "AC"
            };

            AmenityDTO secondAmenityDto = new AmenityDTO
            {
                ID = secondAmenity.Id,
                Name = secondAmenity.Name
            };

            var repositoryAll = BuildRepository();

            AmenityDTO saved1 = await repositoryAll.Create(firstAmenityDto);
            AmenityDTO saved2 = await repositoryAll.Create(secondAmenityDto);

            // Act

            List<AmenityDTO> result = await repositoryAll.GetAmenities();
            

            // Assert

            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task CanDeleteAnAmenity()
        {
            Amenity firstAmenity = new Amenity
            {
                Id = 11,
                Name = "TV"
            };

            AmenityDTO firstAmenityDto = new AmenityDTO
            {
                ID = firstAmenity.Id,
                Name = firstAmenity.Name
            };

            Amenity secondAmenity = new Amenity
            {
                Id = 12,
                Name = "AC"
            };

            AmenityDTO secondAmenityDto = new AmenityDTO
            {
                ID = secondAmenity.Id,
                Name = secondAmenity.Name
            };

            var repositoryAll = BuildRepository();

            AmenityDTO saved1 = await repositoryAll.Create(firstAmenityDto);
            AmenityDTO saved2 = await repositoryAll.Create(secondAmenityDto);

            // Act

            await repositoryAll.Delete(1);
            List<AmenityDTO> result = await repositoryAll.GetAmenities();


            // Assert

            Assert.Equal(4, result.Count);
        }

        //[Fact]
        //public async Task CanUpdateAnAmenity()
        //{
        //    // Arrange

        //    Amenity firstAmenity = new Amenity
        //    {
        //        Id = 11,
        //        Name = "TV"
        //    };

        //    AmenityDTO firstAmenityDto = new AmenityDTO
        //    {
        //        ID = firstAmenity.Id,
        //        Name = firstAmenity.Name
        //    };

        //    Amenity secondAmenity = new Amenity
        //    {
        //        Id = 12,
        //        Name = "AC"
        //    };

        //    AmenityDTO secondAmenityDto = new AmenityDTO
        //    {
        //        ID = secondAmenity.Id,
        //        Name = secondAmenity.Name
        //    };

        //    var repository = BuildRepository();

        //    // Act
        //    AmenityDTO saved = await repository.Create(firstAmenityDto);

        //    await repository.Update(secondAmenityDto);

        //    var result = await repository.GetAmenity(1);

        //    // Assert
        //    Assert.Equal("AC", result.Name);
        //}
    }
}
