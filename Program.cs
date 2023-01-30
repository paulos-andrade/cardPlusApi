using cadPlus_Api.Data;
using EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();


var app = builder.Build();
app.RegisterUsersApis();



app.Run();
