using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokeBattler.Client.Services;
using PokeBattler.Common.Models;
using PokeBattler.Server.Services;
using PokeBattler.Server.Services.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.Configure<GameSettings>(builder.Configuration.GetSection(GameSettings.Key));

// API services
//builder.Services.AddSingleton<GameHub>();
// Services
builder.Services.AddSingleton<HttpService>();
builder.Services.AddSingleton<GameSettings>();
builder.Services.AddSingleton<IArenaService, ArenaService>();
builder.Services.AddSingleton<ITrainersService, TrainersService>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IPokeApiService, PokeAPIService>();
builder.Services.AddSingleton<IPokemonPoolService, PokemonPoolService>();
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddSingleton<IShopService, ShopService>();

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
