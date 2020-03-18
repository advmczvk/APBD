using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
        public Student UpdateStudent(int id, string varType, string value);
        public void DeleteStudent(int id);
    }
}
