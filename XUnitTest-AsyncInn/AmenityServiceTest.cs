using Lab12_Relational_DB.Model;
using Lab12_Relational_DB.Model.Interfaces;
using Lab12_Relational_DB.Model.Services;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using Lab12_Relational_DB.Model.DTOs;

namespace XUnitTest_AsyncInn
{
    public class AmenityServiceTest : DatabaseTestBase
    {
        private IAmenity BuildRepository()
        {
            return new AmenityRepository(_db);
        }

        [Fact]
        public async Task CanSaveAndGet()
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

            Assert.NotNull(saved);
            Assert.NotEqual(0, amenityDto.ID);
            Assert.Equal(saved.ID, amenityDto.ID);
            Assert.Equal(amenity.Name, saved.Name);
        }
    }
}
