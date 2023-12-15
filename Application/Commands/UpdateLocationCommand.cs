using Domain.Models.Requests;
using Domain.Models.Responses;
using FluentResults;
using MediatR;

namespace Application.Commands
{
    /// <summary>
    /// Represents the update command that is being sent to UpdateLocationCommand.
    /// </summary>
    public class UpdateLocationCommand : IRequest<Result<LocationResponse>>
    {
        /// <summary>
        /// Constructor for UpdateLocationCommand class.
        /// </summary>
        /// <param name="location"></param>
        public UpdateLocationCommand(int id, LocationRequest location)
        {
            Location = location;
            Id = id;
        }

        /// <summary>
        /// Gets the Location parameter.
        /// </summary>
        public LocationRequest Location { get; }

        /// <summary>
        /// Gets the Id parameter.
        /// </summary>
        public int Id { get; }
    }
}
