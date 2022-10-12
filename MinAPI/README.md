# .NET Minimal API Example


## Definition

Minimal APIs are a way to create HTTP APIs with minimal dependencies.

Some missing features, comparing with the normal WebAPI:

- Absense of the controller class (typical from MVC software architectural pattern).

- It doesn't the MVC framework.

- Don't support model validation. It should be added externally, using libraries like FluentValidation, MiniValidation.

- Don't support JSONPatch, what it will impede the addition of PATCH verb by default.

- Filters absent: hability to allow code to run befor or after specific stages in the request pipeline (authorization, resource, action, exception, result).

- Custom model binding (IModelBinder) absent: the model binder allows mapping between incoming request data and application model (as an enhancement of your model).


## Technologies Used

- .NET 6
 
- Entity Framework 
 
- Repository Pattern

- AutoMapper

- SQL Server?

- Docker


## Commands

Creating the project:

```
dotnet new webapi -minimal -n MinAPI
```


Fix invalid state certificates (HTTPS):

```
dotnet dev-certs https --clean
dotnet dev-certs https --trust
``` 


Running in hot-reload mode:
```
dotnet watch
```


Using User Secrets:

```
dotnet user-secrets init
dotnet user-secrets set "UserId" "sa"  
dotnet user-secrets set "Password" "pa55w0rd;"
builder.Configuration["UserId"]  
```


Docker Command:

```
docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=123ABC%&$' -e 'MSSQL_PID=Developer' -p 1433:1433 --name azuresqledge -d mcr.microsoft.com/azure-sql-edge
```


Migrations:

```
dotnet tool install --global dotnet-ef
dotnet ef migrations initialMigration
dotnet ef database update
```


Example of an endpoint (GET):

```
app.MapGet("api/v1/commands/{id}", async (ICommandRepo repository, IMapper mapper, int id) =>
{
    var command = await repository.GetCommandByIdAsync(id);

    if (command != null)
        return Results.Ok(mapper.Map<CommandReadDto>(command));

    return Results.NotFound();
});
```


## Links

[.NET 6 Minimal API Full Build - Les Jackson](https://www.youtube.com/watch?v=5YB49OEmbbE)


