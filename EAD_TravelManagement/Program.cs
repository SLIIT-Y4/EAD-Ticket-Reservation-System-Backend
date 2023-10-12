/*
 * File: Program.cs
 * Author: Abeywickrama C.P.
 * Date: October 4, 2023
 * Description: This file contains the definition of the services settings, which provides various utility functions.
 */

using EAD_TravelManagement.Models;
using EAD_TravelManagement.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<TicketReservationDatabaseSettings>(
builder.Configuration.GetSection("TicketReservationDatabase"));

builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<ReservationService>();
builder.Services.AddSingleton<TrainsService>();
builder.Services.AddSingleton<SchedulesService>();
builder.Services.AddSingleton<LoginsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EAD", Version = "v1" });
    // Add any additional configurations, such as XML comments, security, etc.
    // c.IncludeXmlComments("path-to-your-xml-comments-file.xml");
});

var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EAD");
    });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
