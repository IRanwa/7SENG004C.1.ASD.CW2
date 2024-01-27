using Microsoft.EntityFrameworkCore;

namespace _7SENG004C._1.ASD.CW2;

/// <summary>
/// The ASD Db context.
/// </summary>
/// <seealso cref="DbContext" />
public class ASDDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ASDDbContext"/> class.
    /// </summary>
    public ASDDbContext()
    {
    }

    /// <summary>
    /// The database configuring.
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("AsdDB");
    }

    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; }
}