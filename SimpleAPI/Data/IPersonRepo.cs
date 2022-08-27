using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleAPI.Models;

namespace SimpleAPI.Data
{
    public interface IPersonRepo
    {
        IEnumerable<Person> GetAllPeople();
        Person GetPersonById(int id);
    }
}