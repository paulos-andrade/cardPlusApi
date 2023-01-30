using cadPlus_Api.Data;
using cadPlus_Api.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();
//Configurações do banco (connection string,etc)

var app = builder.Build();

app.MapGet("v1/getAllUsers", (ApplicationDbContext context) =>
{
    var users = context.Users.ToList();
    return Results.Ok(users);
});

app.MapGet("v1/getUser:{id}", (ApplicationDbContext context) =>
{
    var users = context.Users.ToList();
    return Results.Ok(users);
});

app.MapPost("v1/insertUser", (
    ApplicationDbContext context, CreateUserViewModel userModel) =>
{
    var user = userModel.Validate();

    if (!userModel.IsValid)
    {
        return Results.BadRequest(userModel.Notifications);
    }
    context.Users.Add(user);
    context.SaveChanges();
    return Results.Created($"/v1/insertUser/{user.Id}", user);
});


app.MapPost("/", () => "Cad User");
app.MapGet("/", () => "Hello World!");

app.Run();
