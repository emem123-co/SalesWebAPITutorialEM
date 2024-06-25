using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebAPITutorialEM.Data;
namespace SalesWebAPITutorialEM;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddDbContext<AppdBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AppdBContext")//automatically added when you add conroller and dBContext classes.
            ?? throw new InvalidOperationException("Connection string 'AppdBContext' not found."))); 

        var app = builder.Build();

        builder.Services.AddCors(); //allows you to add limitations on who can access what. we want to remove the limitations with UseCors(); (below).

    // Configure the HTTP request pipeline.
        app.UseCors(x=> x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
