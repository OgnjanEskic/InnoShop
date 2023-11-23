using Application.Queries;
using Domain.Interfaces;
using Domain.Models.Responses;
using MediatR;

namespace Application.Handlers.QueryHandlers
{
    /// <summary>
    /// Represents the handler for GetLocations endpoint.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    internal class GetLocationsQueryHandler :
        IRequestHandler<GetLocationsQuery, GetLocationsQueryResponse>
    {
        private readonly ILocationRepository locationRepository;

        /// <summary>
        /// Constructor for GetLocationsQueryHandler class.
        /// </summary>
        /// <param name="locationRepository"></param>
        public GetLocationsQueryHandler(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        /// <summary>
        /// Gets the list of locations and passes it back to the controller.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetLocationsQueryResponse> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            return await locationRepository.GetLocationsAsync(request.PaginationParameters);
        }
    }
}
