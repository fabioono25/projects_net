using Microsoft.Extensions.Configuration;
using Ordering.API.Data;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

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

app.UseAuthorization();

app.MapControllers();

using var serviceScope = app.Services.CreateScope();
var context = app.Services.GetRequiredService<OrderContext>();
var serviceProvider = serviceScope.ServiceProvider;

PrepDb.MigrateDatabase<OrderContext>((context, ServiceProvider) =>
{
    var logger = app.Services.GetService<ILogger<OrderContextSeed>>();

    OrderContextSeed
        .SeedAsync(context, logger).Wait();

}, app);

app.Run();

