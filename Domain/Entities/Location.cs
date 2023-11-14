using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Represents the entity for creation of Location table.
    /// </summary>
    [Table("Location")]
    [Index(nameof(Name), IsUnique = true)]
    public class Location
    {
        /// <summary>
        /// Gets or sets the Id parameter.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name parameter.
        /// </summary>
        [Required, StringLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Description parameter.
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
