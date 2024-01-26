using System.ComponentModel.DataAnnotations;

namespace _7SENG004C._1.ASD.CW2;

/// <summary>
/// The user.
/// </summary>
/// <seealso cref="EntityBase" />
public class User : EntityBase
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; }
}