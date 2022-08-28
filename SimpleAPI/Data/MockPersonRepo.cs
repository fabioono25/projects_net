using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleAPI.Models;

namespace SimpleAPI.Data
{
    public class MockPersonRepo : IPersonRepo
    {
        public void CreatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public void DeletePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAllPeople() =>
            new List<Person>
            {
                new Person { Id = 1, Name = "James", Age = 23, Identifier = "CAM" },
                new Person { Id = 2, Name = "Adele", Age = 32, Identifier = "ADE" },
                new Person { Id = 3, Name = "Pele", Age = 82, Identifier = "PEL" }
            }; 

        public Person GetPersonById(int id) =>
            new Person { Id = 1, Name = "James", Age = 23, Identifier = "CAM" };

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}