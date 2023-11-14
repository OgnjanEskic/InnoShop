using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    /// <summary>
    /// The Location db context.
    /// </summary>
    public class LocationDbContext : DbContext
    {
        /// <summary>
        /// Constructor for LocationDbContext class.
        /// </summary>
        /// <param name="options"></param>
        public LocationDbContext(DbContextOptions<LocationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the locations.
        /// </summary>
        public DbSet<Location> Locations { get; set; }
    }
}
