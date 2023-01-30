using cadPlus_Api.Data;
using cadPlus_Api.ViewModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();
//Configurações do banco (connection string,etc)

var app = builder.Build();


app.MapGet("v1/getAllUsers", async (ApplicationDbContext context) =>
{
    var users = await context.Users.ToListAsync();
    return Results.Ok(users);

}).Produces<User>(); // retorna o Schema do user

app.MapGet("v1/getUser/{id}", async (int id, ApplicationDbContext context) =>
{
    var users = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
    return Results.Ok(users);

}).Produces<User>();

app.MapPost("v1/insertUser", async (
    ApplicationDbContext context, CreateUserViewModel userModel) =>
{
    var user = userModel.Validate();

    if (userModel.IsValid)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return Results.Created($"/v1/insertUser/{user.Id}", user);
    }
    return Results.BadRequest(userModel.Notifications);
});

app.Run();
