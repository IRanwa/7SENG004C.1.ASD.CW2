using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _7SENG004C._1.ASD.CW2;

/// <summary>
/// The transaction.
/// </summary>
/// <seealso cref="EntityBase" />
public class Transaction : EntityBase
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the category identifier.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the amount.
    /// </summary>
    public double Amount { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public TransactionType Type { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the remark.
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
}