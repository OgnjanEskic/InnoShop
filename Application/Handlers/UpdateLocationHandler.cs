using Application.Commands;
using Domain.Interfaces;
using Domain.Models.Requests;
using Domain.Models.Responses;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    /// <summary>
    /// Represents the handler for UpdateLocationHandler.
    /// </summary>
    public class UpdateLocationHandler : IRequestHandler<UpdateLocationCommand, Result<LocationResponse>>
    {
        private readonly ILogger<UpdateLocationHandler> _logger;
        private readonly IValidator<LocationRequest> _locationRequestValidator;
        private readonly ILocationRepository _locationRepository;

        /// <summary>
        /// Constructor for CreateLocationHandler class.
        /// </summary>
        /// <param name="locationRequestValidator"></param>
        /// <param name="locationRepository"></param>
        /// <param name="logger"></param>
        public UpdateLocationHandler(IValidator<LocationRequest> locationRequestValidator,
            ILocationRepository locationRepository, ILogger<UpdateLocationHandler> logger)
        {
            _locationRequestValidator = locationRequestValidator;
            _locationRepository = locationRepository;
            _logger = logger;
        }

        /// <summary>
        /// Sends object to FluentValidator and verifies returned results via FluentResult.
        /// </summary>
        /// <param name="request">Request with information for edition.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A Task with the proper response code and the Uri of the location object 
        /// and the object itself if the object is edited in the database.</returns>
        public async Task<Result<LocationResponse>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Validating the request data...");
            var validationResult = _locationRequestValidator.Validate(request.Location);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Sending bad request due to request data validation failure, Errors: {errors}", validationResult.Errors);
                Error error = new("Validation Failure");

                foreach (var validationFailure in validationResult.Errors.Select(x => x.ErrorMessage))
                {
                    error.Reasons.Add(new Error(validationFailure));
                }
                return Result.Fail(error);
            }

            _logger.LogInformation("Sending data to edit location...");
            var response = await _locationRepository.UpdateLocationAsync(request.Id, request.Location);
            if (response.IsFailed)
            {
                return response.ToResult();
            }

            return response;
        }
    }
}