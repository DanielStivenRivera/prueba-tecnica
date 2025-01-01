using app_server.Adapters;
using app_server.Application.services;
using app_server.Domain.Entities;
using app_server.Domain.Exceptions;
using app_server.Infrastructure.Persistence;
using app_server.Infrastructure.Persistence.Repositories;
using app_server.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Connect to the database
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        builder.Services.AddScoped<UserRepository>();
        builder.Services.AddScoped<JwtTokenGenerator>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<UserAdapter>();
        
        builder.Services.AddScoped<ReservationRepository>();
        builder.Services.AddScoped<ReservationService>();
        builder.Services.AddScoped<ReservationAdapter>();
        
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.UseMiddleware<ExceptionMiddleware>();

        // Migrate the database
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!dbContext.Database.CanConnectAsync().Result)
        {
            Console.WriteLine("Cannot connect to the database.");
            return;
        }
        using (var scope2 = app.Services.CreateScope())
        {
            var dbContext2 = scope2.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!dbContext2.Database.CanConnectAsync().Result)
            {
                Console.WriteLine("Cannot connect to the database.");
                return;
            }

            dbContext2.Database.EnsureCreated();

            if (!dbContext.Users.Any(u => u.username == "default_user"))
            {
                var hashedPasswordExample = BCrypt.Net.BCrypt.HashPassword("hashedpasswordexample");
                var defaultUser = new User
                {
                    username = "default_user",
                    password = hashedPasswordExample,
                    email = "default@example.com",
                    createdAt = DateTime.UtcNow
                };

                dbContext.Users.Add(defaultUser);
                dbContext.SaveChanges();
            }
        }
        
        AddDefaultSpaces(dbContext);

        app.Run();
    }

    private static void AddDefaultSpaces(ApplicationDbContext dbContext)
    {
        if (!dbContext.Spaces.Any())
        {
            var defaultSpaces = new List<Space>
            {
                new Space { name = "Place 1", capacity = 20 },
                new Space { name = "Place 2", capacity = 30 },
                new Space { name = "Place 3", capacity = 50 },
                new Space { name = "Place 4", capacity = 25 },
                new Space { name = "Place 5", capacity = 45 },
            };
            dbContext.Spaces.AddRange(defaultSpaces);
            dbContext.SaveChanges();
        }
    }
}