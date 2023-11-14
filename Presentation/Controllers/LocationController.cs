using Application.Commands;
using Domain.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// Represents the controller class for Location service.
    /// </summary>
    [Route("api/[controller]")]
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
    }
}
