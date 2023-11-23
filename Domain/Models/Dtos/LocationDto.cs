namespace Domain.Models.Dtos
{
    public class LocationDto
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
