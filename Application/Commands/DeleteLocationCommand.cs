using FluentResults;
using MediatR;

namespace Application.Commands
{
    /// <summary>
    /// Represents the delete command that is being sent to DeleteLocationHandler.
    /// </summary>
    public class DeleteLocationCommand : IRequest<Result>
    {
        /// <summary>
        /// Constructor for DeleteLocationCommand class.
        /// </summary>
        /// <param name="id"></param>
        public DeleteLocationCommand(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the Id parameter.
        /// </summary>
        public int Id { get; }
    }
}
