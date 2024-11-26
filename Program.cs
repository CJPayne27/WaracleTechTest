using HotelWaracleBookingApi.Data;
using HotelWaracleBookingApi.Data.Repositories;
using HotelWaracleBookingApi.Exceptions;
using HotelWaracleBookingApi.Services;
using HotelWaracleBookingApi.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DatabaseSeeder = HotelWaracleBookingApi.Data.Seeding.DatabaseSeeder;

namespace HotelWaracleBookingApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddDbContext<HotelWaracleDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "HotelWaracleBookingApi", Version = "v1", Description = "CJ Grant Tech Test"});
            c.MapType<ProblemDetails>(() => new OpenApiSchema
            {
                Type = "object",
                Properties =
                {
                    ["title"] = new OpenApiSchema { Type = "string" },
                    ["status"] = new OpenApiSchema { Type = "integer", Format = "int32" },
                    ["detail"] = new OpenApiSchema { Type = "string" },
                    ["instance"] = new OpenApiSchema { Type = "string" }
                }
            });
        });

        builder.Services.AddLogging();

        builder.Services.AddSingleton<IExceptionHandler, GlobalExceptionHandler>();

        builder.Services.AddScoped<DatabaseSeeder>();
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
        builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
        builder.Services.AddScoped<IHotelRepository, HotelRepository>();
        builder.Services.AddScoped<IHotelService, HotelService>();
        builder.Services.AddScoped<IBookingRepository, BookingRepository>();
        builder.Services.AddScoped<IBookingService, BookingService>();
        builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler(appBuilder =>
        {
            appBuilder.Run(async context =>
            {
                var exceptionHandler = context.RequestServices.GetRequiredService<IExceptionHandler>();
                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (exceptionFeature != null)
                {
                    var exception = exceptionFeature.Error;
                    var handled = await exceptionHandler.TryHandleAsync(context, exception, context.RequestAborted);
                    if (!handled)
                    {
                        throw exception; // If not handled, rethrow
                    }
                }
            });
        });

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}