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

        public void CreatePerson(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            _context.People.Add(person);
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return _context.People.ToList();
        }

        public Person GetPersonById(int id) =>
            _context.People.FirstOrDefault(p => p.Id == id);

        public void UpdatePerson(Person person)
        {
            // nothing in the way that it is implemented - DbContext will take care of it
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void DeletePerson(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            _context.People.Remove(person);
        }
    }
}

