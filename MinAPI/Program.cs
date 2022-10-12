using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MinAPI.Data;
using MinAPI.Dtos;
using MinAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConnBuilder = new SqlConnectionStringBuilder();
sqlConnBuilder.ConnectionString = builder.Configuration.GetConnectionString("SQLDbConnection");
sqlConnBuilder.UserID = builder.Configuration["UserId"];
sqlConnBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConnBuilder.ConnectionString));
builder.Services.AddScoped<ICommandRepo, CommandRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("api/v1/commands", async (ICommandRepo repository, IMapper mapper) =>
{
    var commands = await repository.GetAllCommandsAsync();

    return Results.Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
});

app.MapGet("api/v1/commands/{id}", async (ICommandRepo repository, IMapper mapper, int id) =>
{
    var command = await repository.GetCommandByIdAsync(id);

    if (command != null)
        return Results.Ok(mapper.Map<CommandReadDto>(command));

    return Results.NotFound();
});

app.MapPost("api/v1/commands", async (ICommandRepo repository, IMapper mapper, CommandCreateDto cmdCreateDto) =>
{
    var commandModel = mapper.Map<Command>(cmdCreateDto);

    await repository.CreateCommandAsync(commandModel);
    await repository.SaveChangesAsync();

    var cdmReadDto = mapper.Map<CommandReadDto>(commandModel);

    return Results.Created($"api/v1/commands/{commandModel.Id}", cmdCreateDto);
});

app.MapPut("api/v1/commands/{id}", async (ICommandRepo repository, IMapper mapper, int id, CommandUpdateDto cmdUpdateDto) =>
{
    var command = await repository.GetCommandByIdAsync(id);

    if (command == null)
        return Results.NotFound();

    mapper.Map(cmdUpdateDto, command);
    await repository.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("api/v1/commands/{id}", async (ICommandRepo repository, IMapper mapper, int id) =>
{
    var command = await repository.GetCommandByIdAsync(id);

    if (command == null)
        return Results.NotFound();

    repository.DeleteCommand(command);
    await repository.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
