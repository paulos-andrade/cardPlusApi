using cadPlus_Api.Data;
using EndPoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:4200",
                                              "http://localhost:4200").AllowAnyHeader()
                                              .AllowAnyMethod()
                                              .AllowAnyOrigin() ;
                      });
});


builder.Services.AddDbContext<ApplicationDbContext>(); //Adiciona contexto do banco
var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins); //cors

app.RegisterUsersApis(); // registra os Endpoints



app.Run();
