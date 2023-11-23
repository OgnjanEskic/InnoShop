using Domain.Models.Dtos;

namespace Domain.Models.Responses
{
    /// <summary>
    /// Represents the db response where the list is filled with the locations returned from the database
    /// </summary>
    public class GetLocationsQueryResponse
    {
        /// <summary>
        /// Gets or sets the Locations parameter.
        /// </summary>
        public List<LocationDto> Locations { get; set; } = new();

        /// <summary>
        /// Gets or sets the PageNumber parameter.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the PageSize parameter.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the TotalCount parameter.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
