using Microsoft.AspNetCore.Mvc;
using officeUsage.DataAccess;
using officeUsage.DomainObjects;

namespace officeUsage.Controllers;

/// <summary>
/// Class that handles room operations
/// </summary>
[ApiController]
[Route("~/room")]
public class RoomController : ControllerBase
{
    private DatabaseContext DatabaseContext { get; }

    /// <summary>
    /// Handles in-memory-database
    /// </summary>
    /// <param name="dbc"></param>
    public RoomController(DatabaseContext dbc)
    {
        DatabaseContext = dbc;
    }

    /// <summary>
    /// HTTP-GET-Method to get all rooms
    /// </summary>
    /// <returns>All users.</returns>
    [HttpGet]
    public ActionResult<Room[]> GetAllRooms()
    {
        return DatabaseContext.Rooms.ToArray();
    }
    
    /// <summary>
    /// HTTP-GET-Method to get specific room details
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    [HttpGet("{roomId}")]
    public ActionResult<Room[]> GetRoom([FromRoute] int roomId)
    {
        var room = DatabaseContext.Rooms.Find(roomId);
        if (room is null) return NotFound();
        return Ok(room);
    }

    /// <summary>
    /// HTTP-POST-Method to create a new room
    /// </summary>
    /// <param name="room">A fully-specified room</param>
    /// <returns>A HTTP-response</returns>
    [HttpPost]
    public ActionResult<Room> CreateRoom(Room room)
    {
        if (ModelState.IsValid is false) return BadRequest(ModelState);
        Room currentRoom = DatabaseContext.Rooms.FirstOrDefault(r => r.RoomId == room.RoomId);
        if (currentRoom != null) return Conflict("Room is already allocated");
        DatabaseContext.Rooms.Add(room);
        DatabaseContext.SaveChanges();
        return Ok(room);
    }

    /// <summary>
    /// An HTTP-PUT-method to update room details
    /// </summary>
    /// <param name="room">A fully specified room.</param>
    /// <returns>A HTTP-response</returns>
    [HttpPut]
    public ActionResult<Room> UpdateRoom(Room room)
    {
        if (ModelState.IsValid is false) return BadRequest(ModelState);
        Room currentRoom = DatabaseContext.Rooms.Find(room.RoomId);
        if (currentRoom is null) return NotFound();
        DatabaseContext.Rooms.Update(currentRoom);
        DatabaseContext.SaveChanges();
        return Ok(room);
    }

    /// <summary>
    /// An HTTP-DELETE-method to delete a room from the database.
    /// </summary>
    /// <param name="roomId">Just the roomId.</param>
    /// <returns>A HTTP-response</returns>
    [HttpDelete("{roomId}")]
    public ActionResult<Room> DeleteRoom([FromRoute]int roomId)
    {
        var room = DatabaseContext.Rooms.Find(roomId);
        if (room is null) return NotFound();
        DatabaseContext.Rooms.Remove(room);
        DatabaseContext.SaveChanges();
        return Ok(room);
    }
    
}