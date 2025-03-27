using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoChess.Library.Extensions;
using AutoChess.Server.Services;
using AutoChess.Contracts.Options;
using AutoChess.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddOptions<IGameOptions>()
    .Bind(builder.Configuration.GetSection(IGameOptions.Key))
    .ValidateDataAnnotations();
builder.Services.AddOptions<IPoolOptions>()
    .Bind(builder.Configuration.GetSection(IPoolOptions.Key))
    .ValidateDataAnnotations();

// API services
//builder.Services.AddSingleton<GameHub>();
// Services
builder.Services.UseHttpInfrastructure();
builder.Services.UseAutoChessServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();

//app.MapControllers();

app.MapHub<GameHub>($"/{GameHub.HubName}");
Console.WriteLine($"SignalR hub listening at '/{GameHub.HubName}'");

app.Run();
Console.WriteLine("Server is now running");
