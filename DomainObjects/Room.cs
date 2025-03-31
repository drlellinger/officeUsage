using System.ComponentModel.DataAnnotations;

namespace officeUsage.DomainObjects;

/// <summary>
/// This class creates the room-scheme
/// </summary>
public class Room
{
    /// <summary>
    /// The primary key.
    /// </summary>
    [Key] public int RoomId { get; set; }
    /// <summary>
    /// The name of the room
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// A description of the room
    /// </summary>
    public string description { get; set; }
    /// <summary>
    /// The storey that the room is located in
    /// </summary>
    public int storey { get; set; }
    /// <summary>
    /// The current allocation of the room
    /// </summary>
    public int currentRoomAllocation { get; set; }
    /// <summary>
    /// The max allocation of the room
    /// </summary>
    public int maxRoomAllocation { get; set; }
}