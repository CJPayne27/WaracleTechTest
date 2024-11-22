using HotelWaracleBookingApi.Data;
using HotelWaracleBookingApi.Data.Repositories;
using HotelWaracleBookingApi.Services;
using HotelWaracleBookingApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using DatabaseSeeder = HotelWaracleBookingApi.Data.Seeding.DatabaseSeeder;

namespace HotelWaracleBookingApi
{
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
                c.SwaggerDoc("v1", new() { Title = "HotelWaracleBookingApi", Version = "v1", Description = "CJ Grant Tech Test "});
            });

            builder.Services.AddLogging();

            builder.Services.AddScoped<DatabaseSeeder>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IHotelRoomsRepository, HotelRoomsRepository>();
            builder.Services.AddScoped<IHotelRoomsService, HotelRoomsService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
