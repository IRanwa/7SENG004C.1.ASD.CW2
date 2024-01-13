using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _7SENG004C._1.ASD.CW2;

/// <summary>
/// The budget.
/// </summary>
/// <seealso cref="EntityBase" />
public class Budget : EntityBase
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the category identifier.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the month.
    /// </summary>
    public int Month { get; set; }

    /// <summary>
    /// Gets or sets the year.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the amount.
    /// </summary>
    public double Amount { get; set; }

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
}