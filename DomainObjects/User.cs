using System.ComponentModel.DataAnnotations;

namespace officeUsage.DomainObjects;

/// <summary>
/// This class creates the user-scheme
/// </summary>
public class User
{
    /// <summary>
    /// The primary-key
    /// </summary>
    [Key] public string userId { get; set; }
    /// <summary>
    /// The user's first name.
    /// </summary>
    public string firstName { get; set; }
    /// <summary>
    /// The user's last name
    /// </summary>
    public string lastName { get; set; }
    /// <summary>
    /// The user's email-adress.
    /// </summary>
    public string email { get; set; }
}