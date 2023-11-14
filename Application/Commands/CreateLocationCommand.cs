using Domain.Models.Requests;
using Domain.Models.Responses;
using FluentResults;
using MediatR;

namespace Application.Commands
{
    /// <summary>
    /// Represents the command (insert or update) that is being sent to CreateLocationHandler.
    /// </summary>
    public class CreateLocationCommand : IRequest<Result<LocationResponse>>
    {
        /// <summary>
        /// Constructor for CreateLocationCommand class.
        /// </summary>
        /// <param name="location"></param>
        public CreateLocationCommand(LocationRequest location)
        {
            Location = location;
        }

        /// <summary>
        /// Gets the Location parameter.
        /// </summary>
        public LocationRequest Location { get; }
    }
}
