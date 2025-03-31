using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using officeUsage.DataAccess;
using officeUsage.DomainObjects;

namespace officeUsage.Controllers;

/// <summary>
/// Class that handles room operations
/// </summary>
[ApiController]
[Route("~/allocation")]
public class AllocationController : ControllerBase
{
    private DatabaseContext DatabaseContext { get; }

    /// <summary>
    /// Handles in-memory-database
    /// </summary>
    /// <param name="dbc"></param>
    public AllocationController(DatabaseContext dbc)
    {
        DatabaseContext = dbc;
    }

    /// <summary>
    /// An HTTP-GET-method to get all allocations.
    /// </summary>
    /// <returns>All allocations.</returns>
    [HttpGet]
    public ActionResult<Allocation[]> GetAllocations()
    {
        return DatabaseContext.Allocations.ToArray();
    }

    /// <summary>
    /// An HTTP-POST-method to create a room allocation.
    /// </summary>
    /// <param name="userId">Just the userId</param>
    /// <param name="roomId">Just the roomId</param>
    /// <param name="hours">How many hours the user plans to stay</param>
    /// <param name="note">An optional note.</param>
    /// <returns>An HTTP-response</returns>
    [HttpPost]
    public ActionResult<Allocation> CreateAllocation([FromQuery][Required] string userId, [FromQuery][Required] int roomId, [FromQuery] int hours, [FromQuery] string note)
    {
        if (DatabaseContext.Rooms.Find(roomId) == null) return BadRequest("Room not found");
        if (DatabaseContext.Users.Find(userId) == null) return BadRequest("User not found");
        
        if (hours <= 0) { hours = 12;}
        
        Allocation allocation = new Allocation()
        {
            Id = userId+"_"+roomId,
            userId = userId,
            roomId = roomId,
            note = note,
            startTime = DateTime.Now,
            endTime = DateTime.Now.AddHours(hours)
        };
        
        Room room = DatabaseContext.Rooms.FirstOrDefault(r => r.RoomId == roomId);
        if (room == null) return BadRequest("Room not found");
        if (room.currentRoomAllocation==room.maxRoomAllocation) return Conflict("Room is fully allocated");
        room.currentRoomAllocation++;
        
        DatabaseContext.Rooms.Update(room);
        DatabaseContext.Allocations.Add(allocation);
        DatabaseContext.SaveChanges();
        return Ok(allocation);
    }
    
    /// <summary>
    /// An HTTP-PUT-method to modify an allocation
    /// </summary>
    /// <param name="userId">Just the userId</param>
    /// <param name="roomId">Just the roomId</param>
    /// <param name="note">A user-defined note</param>
    /// <returns>A HTTP-response</returns>
    [HttpPut]
    public ActionResult<Allocation> UpdateAllocation([FromQuery][Required] string userId, [FromQuery][Required] int roomId, [FromQuery] string note)
    {
        if (DatabaseContext.Rooms.Find(roomId) == null) return BadRequest("Room not found");
        if (DatabaseContext.Users.Find(userId) == null) return BadRequest("User not found");
        
        Allocation allocation = DatabaseContext.Allocations.Find(userId+"_"+roomId);
        if (allocation == null) return BadRequest("Allocation not found");
        
        Allocation newAllocation = new Allocation()
        {
            Id = userId+"_"+roomId,
            userId = userId,
            roomId = roomId,
            note = note,
            startTime = DateTime.Now,
            endTime = allocation.endTime
        };
        
        Room room = DatabaseContext.Rooms.FirstOrDefault(r => r.RoomId == roomId);
        if (room == null) return BadRequest("Room not found");
        if (room.currentRoomAllocation==room.maxRoomAllocation) return Conflict("Room is fully allocated");
        room.currentRoomAllocation++;
        
        DatabaseContext.Rooms.Update(room);
        DatabaseContext.Allocations.Update(newAllocation);
        DatabaseContext.SaveChanges();
        return Ok(newAllocation);
    }

    /// <summary>
    /// An HTTP-DELETE-method to delete an allocation 
    /// </summary>
    /// <param name="userId">Just the userId.</param>
    /// <param name="roomId">Just the roomId</param>
    /// <returns>An HTTP-response</returns>
    [HttpDelete]
    public ActionResult<Allocation> DeleteAllocationByUserIdAndRoomId([FromQuery][Required]String userId, [FromQuery][Required]int roomId)
    {
        if (DatabaseContext.Rooms.Find(roomId) == null) return BadRequest("Room not found");
        if (DatabaseContext.Users.Find(userId) == null) return BadRequest("User not found");
        var allocation = DatabaseContext.Allocations.FirstOrDefault(a => a.Id == userId+"_"+roomId);
        if (allocation == null) return BadRequest("Allocation not found");
        DatabaseContext.Allocations.Remove(allocation);
        
        Room room = DatabaseContext.Rooms.FirstOrDefault(r => r.RoomId == roomId);
        if (room == null) return BadRequest("Room not found");
        room.currentRoomAllocation--;
        DatabaseContext.Rooms.Update(room);
        
        DatabaseContext.SaveChanges();
        return Ok(allocation);
    }
    
}