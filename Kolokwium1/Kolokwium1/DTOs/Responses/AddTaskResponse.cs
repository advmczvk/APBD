using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1.DTOs.Responses
{
    public class AddTaskResponse
    {
        public string name { get; set; }
        public string description { get; set; }
        public DateTime deadline { get; set; }
    }
}
