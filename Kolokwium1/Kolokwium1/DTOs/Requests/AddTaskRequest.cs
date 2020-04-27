using Kolokwium1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1.DTOs.Requests
{
    public class AddTaskRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public DateTime deadline { get; set; }
        public int IdTeam { get; set; }
        public int IdAssignedTo { get; set; }
        public int IdCreator { get; set; }
        public TaskType TaskType { get; set; }
    }
}
