namespace _7SENG004C._1.ASD.CW2;

/// <summary>
/// The entity base.
/// </summary>
public class EntityBase
{
    /// <summary>
    /// Gets or sets a value indicating whether this instance is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the modified date.
    /// </summary>
    public DateTime? ModifiedDate { get; set; }
}