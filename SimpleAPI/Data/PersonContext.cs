using System;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.Models;

namespace SimpleAPI.Data
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> opt): base(opt)
        {
           
        }

        public DbSet<Person> People { get; set; }
    }
}

