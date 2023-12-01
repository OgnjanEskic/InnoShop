using Domain.Models.Requests;
using Domain.Models.Responses;
using FluentResults;

namespace Domain.Interfaces
{
    /// <summary>
    /// Represents the interface for LocationRepository class from Infrastructure layer.
    /// </summary>
    public interface ILocationRepository
    {
        /// <summary>
        /// Calls the method to insert LocationRequest object into the db.
        /// </summary>
        /// <param name="locationRequest">The type of the object to be written.</param>
        /// <returns>A Task with the object that contains ID of the inserted row.</returns>
        public Task<Result<LocationResponse>> CreateLocationAsync(LocationRequest locationRequest);

        /// <summary>
        ///  Asynchronous method that gets a list of all locations from the database.
        /// </summary>
        /// <param name="getLocationsRequest"></param>
        /// <returns>A Task with the object that contains a list of all locations.</returns>
        public Task<GetLocationsQueryResponse> GetLocationsAsync(GetLocationsRequest getLocationsRequest);

        /// <summary>
        /// Calls the method to delete Location object from the db.
        /// </summary>
        /// <param name="id">The ID of the object to be deleted.</param>
        /// <returns>A Task with the object that contains the ID of deleted row.</returns>
        public Task<Result> DeleteLocationAsync(int id);
    }
}
