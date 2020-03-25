using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace cw3.DAL
{
    public class MockDbService : IDbService
    {
        public List<Student> _students;
        public MockDbService()
        {
            _students = new List<Student>();
        }

        public IEnumerable<Student> GetStudents()
        {
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18730;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                _students.Clear();
                com.Connection = client;
                com.CommandText = "select * from Student";
                client.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student()
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        IndexNumber = dr["IndexNumber"].ToString()
                    };
                    
                    _students.Add(st);
                }
            }

            return _students;
        }
        public IEnumerable<Enrollment> GetEnrollment(string index)
        {
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18730;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "select Semester, IdStudy, StartDate from Enrollment where IdEnrollment = (select IdEnrollment from student where IndexNumber = @index)";
                com.Parameters.AddWithValue("index", index);

                client.Open();
                var dr = com.ExecuteReader();
                var enrollments = new List<Enrollment>();
                while (dr.Read())
                {
                    var enrol = new Enrollment()
                    {
                        sem = int.Parse(dr["Semester"].ToString()),
                        idStud = int.Parse(dr["IdStudy"].ToString()),
                        date = dr["StartDate"].ToString()
                    };
                    
                    enrollments.Add(enrol);
                }
                return enrollments;
            }
        }
        public Student UpdateStudent(string id, string varType, string value)
        {
            
            foreach(var student in _students)
            {
                if(student.IndexNumber == id)
                {
                    if (varType == "name")
                    {
                        student.FirstName = value;
                    }
                    else if(varType == "lname")
                    {
                        student.LastName = value;
                    }
                }
                return student;
            }
            return null; 
        }
        public void DeleteStudent(string id)
        {
            foreach(var student in _students)
            {
                if(student.IndexNumber == id)
                {
                    _students.ToList().Remove(student);
                }
            }
        }

        public Student GetStudent(string index)
        {
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18730;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                _students.Clear();
                com.Connection = client;
                com.CommandText = "select * from Student where IndexNumber = @index";
                com.Parameters.AddWithValue("index", index);
                client.Open();
                var dr = com.ExecuteReader();
                Student st = null;
                while (dr.Read())
                {
                    st = new Student()
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        IndexNumber = dr["IndexNumber"].ToString()
                    };
                }
                
                return st;
            }
        }
    }
}
