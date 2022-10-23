using System;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    // the contract that contains the method to manipulate the DB
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}

