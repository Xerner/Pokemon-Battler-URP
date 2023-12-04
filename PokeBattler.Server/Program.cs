using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PokeBattler.Server.Services;
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

// Services
builder.Services.AddSingleton<HubService>();
builder.Services.AddSingleton<ITrainerService, TrainersService>();
builder.Services.AddSingleton<IAccountService, AccountService>();
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

app.MapControllers();

app.MapHub<HubService>($"/{HubService.HubName}");
Console.WriteLine($"SignalR hub listening at '/{HubService.HubName}'");

app.Run();
Console.WriteLine("Server is now running");
