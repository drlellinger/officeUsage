using Microsoft.EntityFrameworkCore;
using officeUsage.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//API-documentation
builder.Services.AddSwaggerGen(action => {
var xmlFile = "officeUsage.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
action.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseInMemoryDatabase("MyInMemDatabase");
});

var app = builder.Build();

//Init test data if solution is in debug mode
#if DEBUG

using (var scope = app.Services.CreateScope())
{
    using (var dbc = scope.ServiceProvider.GetRequiredService<DatabaseContext>())
    {
        //In DataAccess, dort können Sie Testdaten zur In-Memory-Datenbank hinzufügen
        //DataCreator.InitTestData(dbc);
    }
}
#endif

string roompath = Path.Combine(AppContext.BaseDirectory, "rooms.csv");
string userpath = Path.Combine(AppContext.BaseDirectory, "users.csv");

using (var scope = app.Services.CreateScope())
{
    using (var dbc = scope.ServiceProvider.GetRequiredService<DatabaseContext>())
    {
        DataCreator.ImportUserData(dbc,userpath);
        DataCreator.ImportRoomData(dbc,roompath);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
