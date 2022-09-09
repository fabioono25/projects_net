# .NET Microservices

This code was constructed based on the course [.NET Microservices - Full Course, by Les Jackson](https://www.youtube.com/watch?v=DgVjEo3OGBI&t=35062s)

## Technologies

- Microservices
- Repository Pattern
-
- Using in memory database
- Seed data without migrations
- Using DTOs (read and create)
- Mapping DTOs and Models using AutoMapper
- Using Synchronous and Asynchronous communication approach between microservices
- Publishing in DockerHub
- Using Message Bus to communicate between two microservices
- HttpClient and HttpClientFactory
- Configuring multiple environments via appsettings
- Ingress Nginx Container and Load Balancer (external calls)    
- Using a connection with SqlServer (running via Docker in a Kubernetes pod)
- Using Migrations (code first)    
- gRPC with TLC


## Tools/Frameworks

- .NET 6
- Visual Studio 2022 for Mac
- Insomnia
- Docker
- Kubernetes
- RabbitMQ
- SQL Server

## Developed Services

- Platform Service: asset register, track all platforms/systems of the company
- Commands Service: repository of commands for a given Platform

The objective of this solution is to communicate between the two services. It means that, despite of the fact the two services are independent, they need to communicate between each other someway, in order to get information. Example: Command Service should retrieve information present in Platform Service domain, where we can have a list of commands.

They are two ways to do that: 
1. Coupling Command and Platform using HTTP
2. Using a MessageBus (via Pub/Sub), eliminating the coupling and relying on event consistency.
3. Using synchronous gRPC approach to pull information from Platform to Command service.

## Solution Architecture

![image](https://github.com/fabioono25/projects_net/blob/main/MicroservicesCommunication/Assets/SolutionArchitecture3.png)

### Platform Service Architecture

![image](https://github.com/fabioono25/projects_net/blob/main/MicroservicesCommunication/Assets/PlatformServiceArchitecture.png)

### Command Service Architecture

![image](https://github.com/fabioono25/projects_net/blob/main/MicroservicesCommunication/Assets/CommandServiceArchitecture.png)

### Kubernetes Architecture

![image](https://github.com/fabioono25/projects_net/blob/main/MicroservicesCommunication/Assets/KubernetesArchitecture.png)


## Construction Steps

1. Bootstrap the PlatformService project + adding context/repository capabilities
2. 

## .NET Commands

Basic commands:

````
dotnet new webapi -n PlatformService
dotnet add package <<packageName>>
````


## Docker Commands

[After configuring the Dockerfile with the build and publish commands]

Creating an image:

````
docker build -t <your dockerhub id>/platformservice .
````

Run the image as container:

````
docker run -p 8080:80 -d <docker hub id>/platformservice
````

Show runnig containers + stop + start:

````
docker ps
docker stop <id>
docker ps --all
docker start <id>
````

Push your container up to DockerHub:

````
docker login -u "myusername" -p "mypassword" docker.io

docker push <DockerHub id>/platformservice
````


## Kubernetes Commands


Deploying a container image in a Kubernetes pod (platforms-depl.yaml):

````
kubectl apply -f platforms-depl.yaml
````

Verify de deployments and pods:

````
kubectl get deployments
kubectl get pods
````



## Other Notes

````
	// creating a context
    public class AppDbContext : DbContext // EntityFramework.Core
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<Platform> Platforms { get; set; }
    }

    builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("InMemoryDb"));
````

```
//using context from outside
var serviceScope = app.ApplicationServices.CreateScope();
            Seed(serviceScope.ServiceProvider.GetService<AppDbContext>());
```


## Links

[Dockerize an ASP.NET Core application](https://docs.docker.com/samples/dotnetcore/)









