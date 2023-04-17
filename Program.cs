using TrainAPI.Entities;
using Microsoft.EntityFrameworkCore;
using TrainAPI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<TrainDbSettings>(
    builder.Configuration.GetSection("TrainDbSettings"));

builder.Services.AddSingleton<TrainService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyHeader()
      .AllowAnyMethod()
      .WithOrigins("http://127.0.0.1:5173"));

app.UseAuthorization();
app.MapControllers();

app.Run();
