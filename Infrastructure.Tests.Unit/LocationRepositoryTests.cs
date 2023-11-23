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

        [Test]
        public async Task GetLocationsAsync_WithValidPaginationParameters_ReturnsCorrectPaginatedData()
        {
            //Arrange
            _locationDbContext.Locations.AddRange(
                new Location { Name = "Location1", Description = "Desc1" },
                new Location { Name = "Location2", Description = "Desc2" },
                new Location { Name = "Location3", Description = "Desc3" }
                );

            await _locationDbContext.SaveChangesAsync();

            //Act
            var response = await _locationRepository.GetLocationsAsync(new GetLocationsRequest { PageNumber = 1, PageSize = 2 });

            // Assert
            Assert.That(response.Locations, Has.Count.EqualTo(2));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _locationDbContext.Database.EnsureDeleted();
        }
    }
}