// Learn more about  Clean Architecture at  https://binarybytez.com/understanding-clean-architecture/ 
// Learn more about boilerplate api structure at  https://github.com/kawser2133/clean-structured-api-project/tree/development/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Learn more about Fake data generation at  https://github.com/bchavez/Bogus
// Learn more about EF Migration at https://learn.microsoft.com/en-US/ef/core/managing-schemas/migrations/managing?tabs=dotnet-core-cli

// CustomerViewModel = Dtos  same meamning

using Beer2beer.API.Controllers;
using Beer2beer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlite(builder.Configuration
                .GetConnectionString("PrimaryDbConnection")));

// Register ILogger service
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq(builder.Configuration.GetSection("Seq"));
});

builder.Services.RegisterService();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRequestResponseLogging(); // Add Middleware next for controllers

app.Run();
