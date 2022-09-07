#About

This is a project based on the course [.NET Core 3.1 MVC REST API - Les Jackson](https://www.youtube.com/watch?v=fmvcAzHpsk8&t=8743s).


## Technologies Used

- .NET 6
- AutoMapper
- Entity Framework 6
- Repository Pattern
- PostgreSQL
- Migrations

## Application Architecture

![image](https://github.com/fabioono25/projects_net/blob/main/SimpleAPI/Assets/ApplicationArchitectureSimpleMicroservice.png)


## Adding the Database

I am using a simple instance of Postgres database. Here is an example of command:

```
docker run --name postgresql -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=admin -p 5432:5432 -v /data:/var/lib/postgres/data -d postgres
```


## Example of body for PATCH

```
[
  {
    "path": "/age",
    "op": "replace",
    "value": "2"
  }
]
``