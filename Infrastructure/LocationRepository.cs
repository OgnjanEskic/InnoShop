using Domain.Entities;
using Domain.Interfaces;
using Domain.Models.Requests;
using Domain.Models.Responses;
using FluentResults;
using Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    /// <summary>
    /// Represents the class for calling the implementation of a repository.
    /// </summary>
    public class LocationRepository : ILocationRepository
    {

        private readonly LocationDbContext _locationDbContext;

        /// <summary>
        /// Constructor for LocationRepository class to implement dependency injection for
        /// DbContext implementation held in Infrastructure project.
        /// </summary>
        /// <param name="locationDbContext"></param>
        public LocationRepository(LocationDbContext locationDbContext)
        {
            _locationDbContext = locationDbContext;
        }

        /// <summary>
        /// Calls the method to insert Location object into the db.
        /// </summary>
        /// <param name="locationRequest">The type of the object to be written.</param>
        /// <returns>A Task with the object that contains ID of the inserted row.</returns>
        public async Task<Result<LocationResponse>> CreateLocation(LocationRequest locationRequest)
        {
            Location location = new()
            {
                Description = locationRequest.Description,
                Name = locationRequest.Name
            };

            try
            {
                await _locationDbContext.Locations.AddAsync(location);
                await _locationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException exception && exception.Number == 2601)
                {
                    return Result.Fail(new Error("Duplicated key")
                        .CausedBy("Can not insert duplicated key into the database."));
                }
                throw;
            }

            LocationResponse locationResponse = new()
            {
                Id = location.Id,
                Name = location.Name,
                Description = location.Description
            };

            return Result.Ok(locationResponse);
        }

        ///<inheritdoc/>
        public async Task<GetLocationsQueryResponse> GetLocationsAsync(GetLocationsRequest getLocationsRequest)
        {
            var locations = await _locationDbContext.Locations
                        .Skip((getLocationsRequest.PageNumber - 1) * getLocationsRequest.PageSize)
                        .Take(getLocationsRequest.PageSize)
                        .ToListAsync();

            var totalCount = await _locationDbContext.Locations.CountAsync();

            var response = new GetLocationsQueryResponse()
            {
                PageSize = getLocationsRequest.PageSize,
                PageNumber = getLocationsRequest.PageNumber,
                TotalCount = totalCount
            };

            foreach (var location in locations)
            {
                response.Locations.Add(new()
                {
                    Name = location.Name,
                    Description = location.Description
                });
            }

            return response;
        }
    }
}
