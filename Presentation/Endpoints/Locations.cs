using Application.Commands;
using Carter;
using Domain.Models.Requests;
using MediatR;
using Presentation.Extensions;

namespace Presentation.Endpoints
{
    /// <summary>
    /// Represents the class for Minimal API implementation of Location endpoints.
    /// </summary>
    public class Locations : ICarterModule
    {
        ///<inheritdoc/>
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var locationRoute = app.MapGroup("/api/locations");

            locationRoute.MapPut("{id}", UpdateLocation);
        }

        /// <summary>
        /// Sends a command for update through a MinimalAPI.
        /// </summary>
        ///  <param name="id">ID of the object to be queried.</param>
        /// <param name="locationRequest">An object for the update.</param>
        /// <param name="sender">Instance of the <see cref="MediatR"/> for sending commands/requests.</param>
        /// <returns>IResult object for MinimalAPI usage that contains HTTP response code 
        /// and list of errors that occurred.</returns>
        public static async Task<IResult> UpdateLocation(int id, LocationRequest locationRequest, ISender sender)
        {
            UpdateLocationCommand createLocationCommand = new(id, locationRequest);
            var updateLocationResponse = await sender.Send(createLocationCommand);
            if (updateLocationResponse.IsSuccess)
            {
                return Results.Ok(updateLocationResponse.Value);
            }
            return updateLocationResponse.MapErrorsToMinimalApiResponse();
        }
    }
}
