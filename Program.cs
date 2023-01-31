using cadPlus_Api.Data;
using EndPoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(); //Adiciona contexto do banco
var app = builder.Build();

app.RegisterUsersApis(); // registra os Endpoints



app.Run();
