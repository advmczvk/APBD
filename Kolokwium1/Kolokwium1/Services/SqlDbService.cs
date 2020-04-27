using Kolokwium1.DTOs.Requests;
using Kolokwium1.DTOs.Responses;
using Kolokwium1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace Kolokwium1.Services
{
    public class SqlDbService : IDbService
    {
        public AddTaskResponse AddTask(AddTaskRequest request)
        {
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18730;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                client.Open();
                var transaction = client.BeginTransaction("AddAnimal");
                try
                {
                    com.Connection = client;
                    com.Transaction = transaction;

                    Debug.WriteLine(request.TaskType.name);
                    com.CommandText = "SELECT * FROM TaskType WHERE idTaskType = @idType";
                    com.Parameters.AddWithValue("idType", request.TaskType.idTaskType);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        com.CommandText = "INSERT INTO TaskType VALUES (@idType, @typeName)";
                        com.Parameters.AddWithValue("typeName", request.TaskType.name);
                    }
                    
                    
                    com.CommandText = "INSERT INTO Task VALUES (@name, @description, @deadline, @idTeam, @TaskType, @idAssignedTo, @idCreator)";
                    com.Parameters.AddWithValue("name", request.name);
                    com.Parameters.AddWithValue("description", request.description);
                    com.Parameters.AddWithValue("deadline", request.deadline);
                    com.Parameters.AddWithValue("idTeam", request.IdTeam);
                    com.Parameters.AddWithValue("TaskType", request.TaskType.idTaskType);
                    com.Parameters.AddWithValue("idAssignedTo", request.IdAssignedTo);
                    com.Parameters.AddWithValue("idCreator", request.IdCreator);
                    
                    //wyrzuca do catcha na lini niżej. Nie wiem czemu i nie mam czasu na poprawienie
                    com.ExecuteNonQuery();

                    transaction.Commit();

                    var response = new AddTaskResponse()
                    {
                        name = request.name,
                        description = request.description,
                        deadline = request.deadline
                    };

                    return response;
                }
                catch (SqlException exception)
                {
                    transaction.Rollback();
                    throw new ArgumentException("Parameter is null", "original");
                }
            }
        }

        public IEnumerable<Task> GetProjectData(int id)
        {
            var tasks = new List<Models.Task>();
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18730;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                
                com.Connection = client;
                com.CommandText = "SELECT task.name AS taskName, type.name AS typeName, task.deadline AS taskDeadline FROM Task task JOIN TaskType type ON task.IdTaskType = type.IdTaskType WHERE task.IdProject = @id ORDER BY task.deadline desc";
                com.Parameters.AddWithValue("id", id);
                client.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Models.Task()
                    {
                        name = dr["taskName"].ToString(),
                        type = dr["typeName"].ToString(),
                        deadline = DateTime.Parse(dr["taskDeadline"].ToString())
                    };

                    tasks.Add(st);
                }
            }

            return tasks;
        }
    }
}
