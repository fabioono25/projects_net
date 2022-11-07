# .NET Microservices

This code was constructed based on the articles [Microservices Architecture on .NET - Mehmet Ozkaya](https://medium.com/aspnetrun/microservices-architecture-on-net-3b4865eea03f)

## Technologies & Concepts

- Microservices.
- Repository Pattern.
- API Gateway.
- gRPC.
- DDD
- Clean Architecture
- SOLID
- CQRS


## Tools/Frameworks

- .NET 6.
- Visual Studio 2022 for Mac.
- Docker.
- Swagger.
- MongoDB (document-oriented database, based on BSON-Binary JSON format).
- Redis.
- PostgeSQL.
- Dapper.
- MassTransit.
- RabbitMQ.
- Portainer.
- AutoMapper.


## Solution Architecture

![image](https://github.com/fabioono25/projects_net/blob/main/MicroservicesEcommerce/assets/architecture.png)

## Developed Services


### Catalog.API Service

The Catalog API will implement CRUD operations over Product and Category, using MongoDB database.

The microservices n-layer architecture in general is formed by these 3 layers:

- Data Access: DB operations performed. 
- Business Layer: business logic implemented in this layer.
- Presentation Layer: where the user interacts with.

The Catalog microservices is going to use the layered architecture:

- Domain Layer: business rules and logic.
- Application Layer: expose endpoints and validations. API layer: controllers.
- Infra Layer: persistence operations.


### Basket.API Service

The Basket API will be responsible for managing basket and basket items.

The data will be saved in Redis. Redis is an in-memory, key-value NoSQL database. It is fast and works synchronously. It is ideal for working with distributed caching approach.

Discount will be calculated using gRPC inter-service synchronous communication.

Checkout will be implemented using RabbitMQ and MassTransit.

### Discount.API Service (temporary project)

The main feature of Discount API is the synchronous communication via gRPC with Basket API. PostgreSQL is used as the database for this project.

For this project, the Coupon table was created manually.

Dapper is the micro-ORM for the communication with PostgreSQL.

This project will be replaced by the Discount.Grpc Service.


### Discount.Grpc Service

The main feature of Discount Grpc is to provide a synchronous microservice communication with low latency and high throughput.

```
Update-Package --ProjectName Discount.Grpc
```

AutoMapper tip:
```
CreateMap<Coupon, CouponModel>().ReverseMap();
```

### Ordering.API Service

The Ordering Service is developed using Clean Architecture. They are 4 projects composing this Service:

- Ordering.Domain (Core): no dependencies. Composed of Domain Services and distributed through Strategy Pattern, for example.
- Ordering.Application (Core): it is a "bridge" between Domain layer and other layers. It is an Orcherstration layer. Depends on Domain.
- Ordering.Infrastructure: external dependencies. Dependent on Application layer. Depends on Application Layer.
- Ordering.API: presentation layer. Depends on Application and Infrastructure layers.


### Clean Architecture


Clean architecture is a software design philosophy that separates the elements of a design into ring levels. An important goal of clean architecture is to provide developers with a way to organize code in such a way that it encapsulates the business logic but keeps it separate from the delivery mechanism.

![](https://github.com/fabioono25/projects_net/blob/main/MicroservicesEcommerce/assets/cleanarchitecture.png)


### API Gateway Architecture



## Construction Steps

1. 

## .NET Commands

## Docker Commands

```
```

## Kubernetes Commands

## Other Notes

## MongoDB

Document-oriented database. Every record in MongoDB is a document. Documents are stored in JSON-like Binary JSON (BSN). It uses collections instead of tables and Documents instead of rows.

Working with MongoDB:

```
docker run -d -p 27017:27017 --name test-mongo mongo:latest
docker exec -it test-mongo bash
mongosh
show databases
use CatalogDb
db.getCollectionNames()
db.Products.help()
db.Products.find()
```

## Redis

```
docker pull redis
docker run -d -p 6379:6379 --name test-redis redis
docker logs -f test-redis
docker exec -it test-redis /bin/bash
> redis-cli
> ping
> set key TestKey
> get key
> set name Josh
> get name
```

## Portainer

Container management tool. Manage environments, deploy applications, monitor app performance, triangle problems.

Portainer runs as a Docker container.

## PostgreSQL

- Open-source Relational Database.
- Reliability and flexibility.
- ACID compliance.
- Good performance.

## Dapper

Popular and simple object mapping tool (micro-ORM). It maps queries to objects and, because of its low-level characteristics, it is very fast.


### gRPC (Google Remote Procedure Call)

gRPC is an open-source RPC architecture designed by Google to achieve high-speed communication between microservices. gRPC allows developers to integrate services programmed in different languages. gRPC uses the Protobuf (protocol buffers) messaging format, which is a highly-packed, highly-efficient messaging format for serializing structured data. For a specific set of use-cases, a gRPC API can serve as a more efficient alternative to a REST API (more on this later).

![image](https://github.com/fabioono25/projects_net/blob/main/MicroservicesEcommerce/assets/grpc_rest.png)

### CQRS Pattern

CQRS stands for Command and Query Responsibility Segregation, a pattern that separates read and update operations for a data store. Implementing CQRS in your application can maximize its performance, scalability, and security. The flexibility created by migrating to CQRS allows a system to better evolve over time and prevents update commands from causing merge conflicts at the domain level.

![image](https://github.com/fabioono25/projects_net/blob/main/MicroservicesEcommerce/assets/cqrs.png)

## Links

[Configurate PgAdmin4 using Docker](https://www.pgadmin.org/docs/pgadmin4/latest/container_deployment.html#environment-variables)

[Learn Dapper](https://www.learndapper.com/)

[Clean Architecture - by Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

[Clean Architecture - by Jacobs Data Solutions](https://blog.jacobsdata.com/2020/02/19/a-brief-intro-to-clean-architecture-clean-ddd-and-cqrs)

## Observations

- MongoDB: The mongo command was removed from MongoDB 6.0 and above. The replacement is mongosh.

- Solution if you have the error bellow, trying to integrate the Basket.API with Discount.Grpc:

> Error starting gRPC call.
> 
> System.Net.Http.HttpRequestException: An error occurred while sending the request.

The problem occurred when running the WebAPI using .NET 6. Solution (Program.cs in Discount.Grpc project):

```
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// configure Kestrel to setup a HTTP/2 endpoint without TLS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5003, o => o.Protocols = HttpProtocols.Http2);
});
``` 

- Solution if you have the error bellow, trying to integrate Basket.API with Discount.Grpc:

> Grpc.Core.RpcException: Status(StatusCode="Unimplemented", Detail="Bad gRPC response. HTTP status code: 404")
> 

It is possibly some missing DI in your code. In my case it was the mapping of DiscountService in Discount.Grpc:

```
app.MapGrpcService<DiscountService>();
```




