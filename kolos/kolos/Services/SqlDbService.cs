using kolos.DTOs.Requests;
using kolos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace kolos.Services
{
    public class SqlDbService : IDbService
    {
        
        public IEnumerable<Animal> GetAnimals(string sortBy)
        {
            var _animals = new List<Animal>();
            using(var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18730;Integrated Security=True"))
            using(var com = new SqlCommand())
            {
                
                com.Connection = client;
                com.CommandText = "SELECT a.name, a.type, a.admissiondate, o.lastname FROM Animal a JOIN Owner o ON a.IdOwner = o.IdOwner ORDER BY CAST(@sortBy AS VARCHAR)";
                com.Parameters.AddWithValue("sortBy", sortBy);
                client.Open();

                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var animal = new Animal()
                    {
                        name = dr["Name"].ToString(),
                        type = dr["Type"].ToString(),
                        admissionDate = DateTime.Parse(dr["AdmissionDate"].ToString()),
                        owner = dr["LastName"].ToString()
                    };
                    
                    _animals.Add(animal);
                }
            }
            return _animals;    
        }

        public Animal AddAnimal(AddAnimalRequest request)
        {
            var animal = new Animal()
            {
                name = request.name,
                type = request.type,
                admissionDate = request.admissionDate,
                owner = request.owner,
            };
            var procedure = new ProcedureAnimal()
            {

            }
            return animal;
        }
    }
}
