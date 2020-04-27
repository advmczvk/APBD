using kolos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolos.DTOs.Requests
{
    public class AddAnimalRequest
    {
        public string name { get; set; }
        public string type { get; set; }
        public DateTime admissionDate { get; set; }
        public string owner { get; set; }
        public ProcedureAnimal procedure { get; set; }
    }
}
