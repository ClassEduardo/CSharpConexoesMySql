using System.Data;
using ConexoesMySql.Conexoes.Dapper.Factory;
using ConexoesMySql.Conexoes.Dapper.Repo;
using ConexoesMySql.Conexoes.Dapper.RepoBase;
using ConexoesMySql.Conexoes.EntityFramework;
using ConexoesMySql.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

//   <--- IMPLEMENTAÇÃO ENTITY FRAMEWORK --->
// Registrar o contexto no container de injeções do .net
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions =>
        {
            mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null
            );
        }
    );

    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();
});
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

//   <--- IMPLEMENTAÇÃO DAPPER --->
builder.Services.AddSingleton<IDbConnectionFactory, MySqlConnectionFactory>();

builder.Services.AddScoped<IRepositoryBaseDapper<Turma>>(provider =>
{
    var factory = provider.GetRequiredService<IDbConnectionFactory>();
    return new RepositoryBaseDapper<Turma>(factory, "Turma");
});

builder.Services.AddScoped<IAlunoRepositoryDapper, AlunoRepositoryDapper>();

var app = builder.Build();

app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
