using kolos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolos.Services
{
    interface IDbService
    {
        public IEnumerable<Animal> GetAnimals(string sortBy); 
    }
}
