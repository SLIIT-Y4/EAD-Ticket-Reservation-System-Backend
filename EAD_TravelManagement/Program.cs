/*
 * File: Program.cs
 * Author: Abeywickrama C.P.
 * Date: October 4, 2023
 * Description: This file contains the definition of the services settings, which provides various utility functions.
 */

using EAD_TravelManagement.Models;
using EAD_TravelManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<TicketReservationDatabaseSettings>(
builder.Configuration.GetSection("TicketReservationDatabase"));

builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<ReservationService>();
builder.Services.AddSingleton<TrainsService>();
builder.Services.AddSingleton<SchedulesService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
