using Domain.Models.Requests;
using Domain.Models.Responses;
using MediatR;

namespace Application.Queries;

/// <summary>
/// Represents the query that is being sent to GetLocationsQueryHandler.
/// </summary>
public class GetLocationsQuery : IRequest<GetLocationsQueryResponse>
{

    /// <summary>
    /// Constructor for the GetLocationsQuery class.
    /// </summary>
    /// <param name="paginationParameters"></param>
    public GetLocationsQuery(GetLocationsRequest paginationParameters)
    {
        PaginationParameters = paginationParameters;
    }

    /// <summary>
    /// Gets the pagination parameters
    /// </summary>
    public GetLocationsRequest PaginationParameters { get; }
}