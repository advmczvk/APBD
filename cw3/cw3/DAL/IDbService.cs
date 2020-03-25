using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents(string index);
        public Student UpdateStudent(string id, string varType, string value);
        public void DeleteStudent(string id);
        IEnumerable<Enrollment> GetStudentEnrollment(string index);
    }
}
