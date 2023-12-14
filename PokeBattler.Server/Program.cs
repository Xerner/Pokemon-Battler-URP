using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PokeBattler.Server.Services;
using PokeBattler.Server.Services.Hubs;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

// API services
//builder.Services.AddSingleton<GameHub>();
// Services
builder.Services.AddSingleton<ITrainersService, TrainersService>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IShopService, ShopService>();
builder.Services.AddSingleton<IPokeApiService, PokeAPIService>();
builder.Services.AddSingleton<IPokemonPoolService, PokemonPoolService>();
builder.Services.AddSingleton<IArenaService, ArenaService>();

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
