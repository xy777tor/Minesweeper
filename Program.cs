using StudioTgTest.MiddleWares;
using StudioTgTest.Models.Interfaces;
using StudioTgTest.Persistance;
using StudioTgTest.Services;
using System.Reflection;

namespace StudioTgTest;

public class Program
{
    private const string Origins = "https://minesweeper-test.studiotg.ru";
    private const string CorsPolicyNameTgStudio = "_studioTgAllowOrigin";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: CorsPolicyNameTgStudio,
                              policy =>
                              {
                                  policy.WithOrigins(Origins)
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader();
                              });
        });

        builder.Services.AddSingleton<IGameRepository, GameRepositoryMock>();
        builder.Services.AddTransient<IGameService, GameService>();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "minesweeper API", Version = "v1" });
            c.IncludeXmlComments(Assembly.GetExecutingAssembly());
        });

        var app = builder.Build();

        app.UseExceptionMiddleware();

        app.UseAuthorization();

        app.UseCors(CorsPolicyNameTgStudio);

        app.MapControllers();

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "minesweeper v1");
        });

        app.Run();
    }
}
