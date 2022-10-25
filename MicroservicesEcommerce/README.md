# .NET Microservices

This code was constructed based on the articles [Microservices Architecture on .NET - Mehmet Ozkaya](https://medium.com/aspnetrun/microservices-architecture-on-net-3b4865eea03f)

## Technologies

- Microservices.
- Repository Pattern.
- API Gateway.
- gRPC


## Tools/Frameworks

- .NET 6.
- Visual Studio 2022 for Mac.
- Docker.
- Swagger.
- MongoDB (document-oriented database, based on BSON-Binary JSON format).
- Redis
- MassTransit
- RabbitMQ


## Solution Architecture

![image](https://github.com/fabioono25/projects_net/blob/main/MicroservicesEcommerce/assets/architecture.png)

## Developed Services


### Catalog.API Service Architecture

The Catalog API will implement CRUD operations over Product and Category, using MongoDB database.

The microservices n-layer architecture in general is formed by these 3 layers:

- Data Access: DB operations performed. 
- Business Layer: business logic implemented in this layer.
- Presentation Layer: where the user interacts with.

The Catalog microservices is going to use the layered architecture:

- Domain Layer: business rules and logic.
- Application Layer: expose endpoints and validations. API layer: controllers.
- Infra Layer: persistence operations.


### Basket.API Service Architecture

The Basket API will be responsible for managing basket and basket items.

The data will be saved in Redis. Redis is an in-memory, key-value NoSQL database. It is fast and works synchronously. It is ideal for working with distributed caching approach.

Discount will be calculated using gRPC inter-service synchronous communication.

Checkout will be implemented using RabbitMQ and MassTransit.


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

### gRPC


## Links

## Observations

- MongoDB: The mongo command was removed from MongoDB 6.0 and above. The replacement is mongosh.

- 

## How to extend it





