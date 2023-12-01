using Domain.Entities;
using Domain.Models.Requests;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tests.Unit
{
    public sealed class LocationRepositoryTests
    {
        private LocationDbContext _locationDbContext;
        private LocationRepository _locationRepository;
        private LocationRequest _locationRequest;
        private Location _location;

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

            _locationRequest = new()
            {
                Name = "name",
                Description = "description",
            };
            _location = new()
            {
                Name = "name",
                Description = "description",
            };
        }

        [Test]
        public async Task InsertLocation_AddLocationToDb_ReturnsInsertedLocationId()
        {
            //Arrange
            var expectedResult = 1;

            //Act
            var result = await _locationRepository.CreateLocationAsync(_locationRequest);

            //Assert
            Assert.That(result.Value.Id, Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task InsertLocation_AddAsyncMethodCheck_ReturnsInsertedLocationId()
        {
            //Arrange
            var expectedResult = 1;

            //Act
            await _locationDbContext.Locations.AddAsync(_location);

            //Assert
            Assert.That(_location.Id, Is.EqualTo(expectedResult));

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

        [Test]
        public async Task DeleteLocation_DeleteLocationFromDb_AssuresInsertedLocationIdIsNotInTheDb()
        {
            //Arrange
            var insertResult = await _locationRepository.CreateLocationAsync(_locationRequest);

            //Act
            var deleteResult = await _locationRepository.DeleteLocationAsync(insertResult.Value.Id);

            //Assert
            Assert.That(deleteResult.IsSuccess, Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteLocation_FindAsyncMethodCheck_AssuresInsertedLocationIdIsFoundInTheDb()
        {
            //Arrange
            var insertResult = await _locationRepository.CreateLocationAsync(_locationRequest);

            //Act
            var findResult = _locationDbContext.Locations.FindAsync(insertResult.Value.Id);

            //Assert
            Assert.That(findResult.IsCompleted, Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteLocation_RemoveMethodCheck_AssuresInsertedLocationIEntityStateIsDeleted()
        {
            var insertResult = await _locationRepository.CreateLocationAsync(_locationRequest);
            var findResult = await _locationDbContext.Locations.FindAsync(insertResult.Value.Id);

            //Act
            var removeResult = _locationDbContext.Remove(findResult!);

            //Assert
            Assert.That(removeResult.State, Is.EqualTo(EntityState.Deleted));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _locationDbContext.Database.EnsureDeleted();
        }
    }
}