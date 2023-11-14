using Domain.Entities;
using Domain.Models.Requests;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tests.Unit
{
    public class LocationRepositoryTests
    {
        private LocationDbContext _locationDbContext;
        private LocationRepository _locationRepository;

        [SetUp]
        public void Setup()
        {
            //By supplying a new service provider for each context,
            //single instance of the database will be created for each test.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var dbContextOptions = new DbContextOptionsBuilder<LocationDbContext>()
                .UseInMemoryDatabase(databaseName: "LocationDbContext")
                .UseInternalServiceProvider(serviceProvider);

            _locationDbContext = new LocationDbContext(dbContextOptions.Options);
            _locationDbContext.Database.EnsureCreated();

            _locationRepository = new LocationRepository(_locationDbContext);
        }

        [Test]
        public async Task InsertLocation_AddLocationToDb_ReturnsInsertedLocationId()
        {
            //Arrange
            LocationRequest location = new()
            {
                Description = "description",
                Name = "name",
            };
            var expectedResult = 1;

            //Act
            var result = await _locationRepository.CreateLocation(location);

            //Assert
            Assert.That(result.Value.Id, Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task InsertLocation_AddAsyncMethodCheck_ReturnsInsertedLocationId()
        {
            //Arrange
            Location location = new()
            {
                Description = "description",
                Name = "name",
            };
            var expectedResult = 1;

            //Act
            await _locationDbContext.Locations.AddAsync(location);

            //Assert
            Assert.That(location.Id, Is.EqualTo(expectedResult));

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _locationDbContext.Database.EnsureDeleted();
        }
    }
}