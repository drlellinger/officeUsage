using System.ComponentModel.DataAnnotations;

namespace officeUsage.DomainObjects;

/// <summary>
/// This class creates the allocation-scheme
/// </summary>
public class Allocation
{
    /// <summary>
    /// The primary key
    /// </summary>
    [Key] public string Id { get; set; }
    /// <summary>
    /// The userId that is allocated.
    /// </summary>
    [Required] public string userId { get; set; }
    /// <summary>
    /// The roomId that is allocated.
    /// </summary>
    [Required] public int roomId { get; set; }
    /// <summary>
    /// An used-defined note.
    /// </summary>
    public string note { get; set; }
    /// <summary>
    /// The startTime that is set by default.
    /// </summary>
    public DateTime startTime { get; set; }
    /// <summary>
    /// A defined endTime
    /// </summary>
    public DateTime endTime { get; set; }
}