using Domain.Models.Requests;

namespace Domain.Models.Responses
{
    /// <summary>
    /// Represents the db response where parameter Id is filled with database inserted row Id value
    /// and the sent object value.
    /// </summary>
    public class LocationResponse : LocationRequest
    {
        /// <summary>
        /// Gets or sets the Id parameter.
        /// </summary>
        public int Id { get; set; }
    }
}
