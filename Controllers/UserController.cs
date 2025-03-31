using System.Linq;
using Microsoft.AspNetCore.Mvc;
using officeUsage.DataAccess;
using officeUsage.DomainObjects;

namespace officeUsage.Controllers;

/// <summary>
/// Class that handeles user operations
/// </summary>
[ApiController]
[Route("~/user")]
public class UserController : ControllerBase
{
    private DatabaseContext DatabaseContext { get; }

    /// <summary>
    /// Handles in-memory-database
    /// </summary>
    /// <param name="dbc"></param>
    public UserController(DatabaseContext dbc)
    {
        DatabaseContext = dbc;
    }

    /// <summary>
    /// HTTP-GET-Method to get all users
    /// </summary>
    /// <returns>All users.</returns>
    [HttpGet]
    public ActionResult<User[]> GetAllUsers()
    {
        return DatabaseContext.Users.ToArray();
    }

    /// <summary>
    /// HTTP-GET-Method to get specific user details
    /// </summary>
    /// <param name="userId">Just the userId</param>
    /// <returns>All the details from the specific user</returns>
    [HttpGet("{userId}")]
    public ActionResult<User> GetUser([FromRoute] string userId)
    {
        User currrentUser = DatabaseContext.Users.FirstOrDefault(u => u.userId == userId);
        if (currrentUser == null) return NotFound();
        return Ok(currrentUser);
    }

    /// <summary>
    /// HTTP-POST-Method to create a new user.
    /// </summary>
    /// <param name="user">Fully described user</param>
    /// <returns>HTTP-Response</returns>
    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        if (ModelState.IsValid is false) return BadRequest(ModelState);
        User currentUser = DatabaseContext.Users.FirstOrDefault(u => u.userId == user.userId);
        if (currentUser != null) return Conflict("User already exists");
        DatabaseContext.Users.Add(user);
        DatabaseContext.SaveChanges();
        return Ok(user);
    }

    /// <summary>
    /// HTTP-PUT-Method to update user details
    /// </summary>
    /// <param name="user">Fully described user</param>
    /// <returns>HTTP-Response</returns>
    [HttpPut]
    public ActionResult<User> UpdateUser(User user)
    {
        if (ModelState.IsValid is false) return BadRequest(ModelState);
        User currentUser = DatabaseContext.Users.FirstOrDefault(u => u.email == user.email);
        if (currentUser == null) return BadRequest("User does not exist");
        DatabaseContext.Users.Update(user);
        DatabaseContext.SaveChanges();
        return Ok(user);
    }

    /// <summary>
    /// HTTP-DELETE-Method to remove User from database
    /// </summary>
    /// <param name="userId">Just the userId</param>
    /// <returns>HTTP-Response</returns>
    [HttpDelete("{userId}")]
    public ActionResult<User> DeleteUser(string userId)
    {
        if (ModelState.IsValid is false) return BadRequest(ModelState);
        User currentUser = DatabaseContext.Users.FirstOrDefault(u => u.email == userId);
        if (currentUser == null) return NotFound("User not found");
        DatabaseContext.Users.Remove(currentUser);
        DatabaseContext.SaveChanges();
        return Ok(currentUser);
    }

}