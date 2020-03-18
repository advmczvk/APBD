using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;
        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student{IdStudent=1, FirstName="Jan", LastName="Kowalski"},
                new Student{IdStudent=2, FirstName="Anna", LastName="Malewski"},
                new Student{IdStudent=3, FirstName="Andrzej", LastName="Andrzejewski"}
            };
        }
        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
        public Student UpdateStudent(int id, string varType, string value)
        {
            
            foreach(var student in _students)
            {
                if(student.IdStudent == id)
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
        public void DeleteStudent(int id)
        {
            foreach(var student in _students)
            {
                if(student.IdStudent == id)
                {
                    _students.ToList().Remove(student);
                }
            }
        }
    }
}
