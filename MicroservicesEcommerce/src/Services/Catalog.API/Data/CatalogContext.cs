using System;
using Catalog.API.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
        // public readonly IMongoCollection<Product> Products;

        public CatalogContext(IOptions<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);

            Products = database.GetCollection<Product>(databaseSettings.Value.CollectionName);
            CatalogContextSeed.SeedData(Products);
        }
    }
}

