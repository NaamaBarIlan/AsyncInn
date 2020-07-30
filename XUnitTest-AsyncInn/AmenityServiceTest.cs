using Lab12_Relational_DB.Model;
using Lab12_Relational_DB.Model.Interfaces;
using Lab12_Relational_DB.Model.Services;
using System;
using Xunit;

namespace XUnitTest_AsyncInn
{
    public class AmenityServiceTest : DatabaseTestBase
    {
        private IAmenity BuildRepository()
        {
            return new AmenityRepository(_db);
        }
        
        [Fact]
        public void CanSaveAndGet()
        {
            // Arrange
            // TODO - convert the DTO

            var amenity = new Amenity
            {
                Id = 22,
                Name = "TV"
            };

            var repository = BuildRepository();

            // Act
            var saved = repository.Create(amenity);

            Assert.NotNull(saved);
            Assert.NotEqual(0, amenity.Id);
            Assert.Equal(saved.Id, amenity.Id);
            Assert.Equal(amenity.Name, saved.Name);
        }
    }
}
