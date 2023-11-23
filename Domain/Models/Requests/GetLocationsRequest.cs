namespace Domain.Models.Requests;

public class GetLocationsRequest
{
    /// <summary>
    /// Gets or sets the PageNumber parameter.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets the PageSize parameter.
    /// </summary>
    public int PageSize { get; set; } = 50;
}
