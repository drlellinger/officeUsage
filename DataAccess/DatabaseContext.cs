using Microsoft.EntityFrameworkCore;
using officeUsage.DomainObjects;

namespace officeUsage.DataAccess;

/// <summary>
/// This class supplies the database-scheme
/// </summary>
public class DatabaseContext : DbContext
{
    /// <summary>
    /// The database-constructor
    /// </summary>
    /// <param name="options"></param>
    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }
    
    // public DbSet<Customer> Customer { get; set; }

    /// <summary>
    /// Link the User-scheme to the database
    /// </summary>
    public DbSet<User> Users { get; set; }
    
    /// <summary>
    /// Link the Room-scheme to the database
    /// </summary>
    public DbSet<Room> Rooms { get; set; }
    
    /// <summary>
    /// Link the allocation-scheme to the database
    /// </summary>
    public DbSet<Allocation> Allocations { get; set;}
    

}