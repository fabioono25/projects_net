using System;
using SimpleAPI.Models;

namespace SimpleAPI.Data
{
    public class PostgresPersonRepo : IPersonRepo
    {
        private readonly PersonContext _context;

        public PostgresPersonRepo(PersonContext context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return _context.People.ToList();
        }

        public Person GetPersonById(int id) =>
            _context.People.FirstOrDefault(p => p.Id == id);
    }
}

