namespace Domain.Models.Requests
{
    /// <summary>
    /// Represents the location request class for the object to be stored into the db.
    /// </summary>
    public class LocationRequest
    {
        /// <summary>
        /// Gets or sets the Name parameter.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description parameter.
        /// </summary>
        public string? Description { get; set; }
    }
}
