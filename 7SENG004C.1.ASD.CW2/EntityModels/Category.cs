using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _7SENG004C._1.ASD.CW2;

/// <summary>
/// The category.
/// </summary>
/// <seealso cref="EntityBase" />
public class Category : EntityBase
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the user.
    /// </summary>
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
}