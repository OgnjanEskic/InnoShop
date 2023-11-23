using Application.Commands;
using Application.Queries;
using Domain.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// Represents the controller class for Location service.
    /// </summary>
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor for LocationController class.
        /// </summary>
        /// <param name="mediator"></param>
        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// HttpPost endpoint which inserts new physical or online store location to the database.
        /// </summary>
        /// <param name="location">Request with information for insertion.</param>
        /// <returns>A Task with the proper response code and the Uri of the location object 
        /// and the object itself if the object is stored in the database.</returns>
        [HttpPost(Name = "CreateLocation")]
        public async Task<IActionResult> CreateLocation([FromBody] LocationRequest location)
        {
            var createLocationCommand = new CreateLocationCommand(location);
            var createLocationResponse = await _mediator.Send(createLocationCommand);

            if (createLocationResponse.IsSuccess)
            {
                return CreatedAtAction(nameof(CreateLocation),
                    new { id = createLocationResponse.Value.Id }, createLocationResponse.Value);
            }

            return createLocationResponse.MapErrorsToResponse();
        }

        /// <summary>
        ///  HttpGet endpoint which gets all locations from the database.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A Task with the proper response code and a list of locations.</returns>
        [HttpGet(Name = "GetLocations")]
        public async Task<IActionResult> GetLocations([FromQuery] GetLocationsRequest request)
        {
            var getLocationsQuery = new GetLocationsQuery(request);

            var getLocationsQueryResponse = await _mediator.Send(getLocationsQuery);

            return Ok(getLocationsQueryResponse);
        }
    }
}