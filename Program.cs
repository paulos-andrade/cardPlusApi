using cadPlus_Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(); 
//Configurações do banco (connection string,etc)

var app = builder.Build();

app.MapGet("v1/getAllUsers", (ApplicationDbContext context) =>{
    var users = context.Users.ToList();
    return Results.Ok(users);
});




app.MapPost("/", () => "Cad User");
app.MapGet("/", () => "Hello World!");

app.Run();
