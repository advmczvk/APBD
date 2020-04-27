using Kolokwium1.DTOs.Requests;
using Kolokwium1.DTOs.Responses;
using Kolokwium1.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Kolokwium1.Services
{
    public interface IDbService
    {
        public IEnumerable<Task> GetProjectData(int id);
        public AddTaskResponse AddTask(AddTaskRequest request);
    }
}
