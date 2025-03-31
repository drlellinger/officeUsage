using System.Text.Json;
using officeUsage.DomainObjects;

namespace officeUsage.DataAccess;

/// <summary>
/// This class creates real datasets
/// </summary>
public class DataCreator
{
    /// <summary>
    /// Adds example-data to the database
    /// </summary>
    /// <param name="context"></param>
    public static void InitTestData(DatabaseContext context)
    {

        context.Users.Add(new()
        {
            userId = "user",
            firstName = "John",
            lastName = "Doe",
            email = "johndoe@example.com",
        });

        context.Rooms.Add(new()
        {
            RoomId = 123,
            storey = 1,
            description = "Room description",
            currentRoomAllocation = 0,
            maxRoomAllocation = 4,
            name = "Office Room",
        });
        
        context.SaveChanges();
    }
    
    /// <summary>
    /// This method imports user-data from a json-file.
    /// </summary>
    /// <param name="context">The database this method writes to.</param>
    /// <param name="path">The path that the data comes from</param>
    public static void ImportUserData(DatabaseContext context,string path)
    {  
        List<User> source;  
        using (StreamReader r = new StreamReader(path))  
        {  
            var json = r.ReadToEnd();  
            source = JsonSerializer.Deserialize<List<User>>(json);  
        } 
        context.Users.AddRange(source);
        context.SaveChanges();
    }
    
    /// <summary>
    /// This method imports room-data from a json-file.
    /// </summary>
    /// <param name="context">The database this method writes to.</param>
    /// <param name="path">The path that the data comes from</param>
    public static void ImportRoomData(DatabaseContext context, string path)
    {  
        List<Room> source;  
        using (StreamReader r = new StreamReader(path))  
        {  
            var json = r.ReadToEnd();  
            source = JsonSerializer.Deserialize<List<Room>>(json);  
        } 
        context.Rooms.AddRange(source);
        context.SaveChanges();
    }
    
}