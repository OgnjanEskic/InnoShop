using Application.Commands;
using Domain.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    /// <summary>
    /// Represents the handler for DeleteLocationController.
    /// </summary>
    public class DeleteLocationHandler : IRequestHandler<DeleteLocationCommand, Result>
    {
        private readonly ILogger<DeleteLocationHandler> _logger;
        private readonly ILocationRepository _locationRepository;

        /// <summary>
        /// Constructor for DeleteLocationHandler class.
        /// </summary>
        /// <param name="locationRepository"></param>
        /// <param name="logger"></param>
        public DeleteLocationHandler(ILocationRepository locationRepository, ILogger<DeleteLocationHandler> logger)
        {
            _locationRepository = locationRepository;
            _logger = logger;
        }

        /// <summary>
        /// Sends object to FluentValidator and verifies returned results via FluentResult.
        /// </summary>
        /// <param name="request">Request with information for insertion.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A Task with the proper response code.</returns>
        public async Task<Result> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sending data to delete a location...");
            var response = await _locationRepository.DeleteLocationAsync(request.Id);

            _logger.LogInformation("Response was successfully received...");

            return response;
        }
    }
}