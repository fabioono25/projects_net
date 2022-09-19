# .NET Microservices

This code was constructed based on the course [.NET Microservices - Full Course, by Les Jackson](https://www.youtube.com/watch?v=DgVjEo3OGBI&t=35062s)

## Technologies

- Microservices
- Repository Pattern
- Using in memory database
- Seed data without migrations
- Using DTOs (read and create)
- Using appSettings configuration file
- Mapping DTOs and Models using AutoMapper
- Using Synchronous and Asynchronous communication approach between microservices
- Publishing in DockerHub
- Using Message Bus to communicate between two microservices
- HttpClient and HttpClientFactory
- Configuring multiple environments via appsettings
- Ingress Nginx Container and Load Balancer (external calls)    
- Using a connection with SqlServer (running via Docker in a Kubernetes pod)
- Using Migrations (code first)    
- Ingress controller for Kubernetes (ingress-nginx)
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

```
dotnet new webapi -n PlatformService
dotnet add package <<packageName>>
dotnet new webapi -n CommandService
```


## Docker Commands

[After configuring the Dockerfile with the build and publish commands]

Creating an image:

```
docker build -t <your dockerhub id>/platformservice .
```

Run the image as container:

```
docker run -p 8080:80 -d <docker hub id>/platformservice
```

Show runnig containers + stop + start:

```
docker ps
docker stop <id>
docker ps --all
docker start <id>
```

Push your container up to DockerHub:

```
docker login -u "myusername" -p "mypassword" docker.io

docker push <DockerHub id>/platformservice
```


## Kubernetes Commands


Deploying a container image in a Kubernetes pod (platforms-depl.yaml):

```
kubectl apply -f platforms-depl.yaml
```

Verify de namespaces, deployments, pods, services, storage class and pvc (Persistent Volume Claim):

```
kubectl get namespace
kubectl get deployments
kubectl get pods
kubectl get services
kubectl delete deployment platforms-depl
kubectl get pods --namespace=ingress-nginx
kubectl get storageclass
kubectl get pvc
```

Working with node ports (routing incoming traffic to your service):

```
kubectl apply -f platforms-np-srv.yaml
kubectl get services # NodePort service running
```

Forcing Kubernetes to refresh some image (after applying some changes):
```
kubectl get deployments
kubectl rollout restart deployment platforms-depl
```

Configuring Ingress-Nginx [a controller for Kubernetes using Nginx as a reverse proxy and load balancer]:

```
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.3.1/deploy/static/provider/aws/deploy.yaml
```

Adding a Kubernetes Secrets:
```
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pwd$%123123"
kubectl create secret generic postgres --from-literal=SA_PASSWORD="pwd"
kubectl get secret
``


## Git Commands

As I originally made a mess and pushed the bin folders to this repository, it was necessary a clean command in order to fix everything accordingly:

```
git rm -r --cached .
git add .
git commit -am "remove ignored files"
git push
```


## Other Notes

```
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
```

```
//using context from outside
var serviceScope = app.ApplicationServices.CreateScope();
            Seed(serviceScope.ServiceProvider.GetService<AppDbContext>());
```

When configuring the ingress-nginx in your machine, don't forget to configure the *hosts file* to be in accordance to the host you have defined in ingress-srv.yaml file:

- For Windows: C:\Windows\System32\Drivers\etc\hosts
- For Mac: /private/etc/hosts

Add the value:  127.0.0.1 microservices.com
 

### Synchronous Messaging

- Request/Response Cycle
- Externally facing services usually synchronous (HTTP requests)
- Create a relationship between services (creating dependencies)
- Approachs: HTTP, gRPC

### Asynchronous Messaging

- No Request/Response Cycle
- Typically used between services
- Event bus often used
- No direct relationship between services
- Approachs: event model (using RabbitMQ as event bus), publish/subscriber
- Mode complex


## Links

[Dockerize an ASP.NET Core application](https://docs.docker.com/samples/dotnetcore/)

[Ingress-nginx](https://github.com/kubernetes/ingress-nginx)

[Deploying SQL containers to a Kubernetes cluster](https://rajanieshkaushikk.com/2021/02/27/how-to-deploy-sql-server-containers-to-a-kubernetes-cluster-for-high-availability/)

[Deploying PostgeSQL on Kubernetes](https://phoenixnap.com/kb/postgresql-kubernetes)

[Deploy an Azure SQL Edge container in Kubernetes] (https://learn.microsoft.com/en-us/azure/azure-sql-edge/deploy-kubernetes)



## Observations

I personally had some issues deploying SqlServer to Kubernetes. At the beginning the problem was because of M1 ARM architecture (and there is no support for SQL Express for this architecture yet). The turnaround was using an Azure SQL Edge image, however I had problems configuring the load balancer, so I made use of Postgres for this project (hopefully upgrading it to SQL Express in a near future).





