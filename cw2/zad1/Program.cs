using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Nancy.Json;

namespace zad1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args.Length);
            var pathCSV = args.Length >= 1 ? args[0] : "data.csv";
            var output = args.Length >= 2 ? args[1] : "result.xml";
            var format = args.Length == 3 ? args[2] : "XML";

            try
            {
                var lines = File.ReadAllLines(pathCSV);


                var students = new HashSet<Student>(new StudentComparer());
                var studies = new HashSet<Study>(new StudiesComparer());

                foreach (var line in lines)
                {
                    var student = line.Split(",");

                    if (student.Length >= 9)
                    {
                        students.Add(new Student()
                        {
                            name = student[0],
                            sname = student[1],
                            studies = student[2],
                            mode = student[3],
                            index = student[4],
                            birthday = DateTime.Parse(student[5]),
                            email = student[6],
                            mothersName = student[7],
                            fathersName = student[8]
                        });
                        Boolean exists = false;
                        foreach (var study in studies)
                        {
                            if (exists = study.name == student[2])
                            {
                                study.studentsCount++;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            studies.Add(new Study()
                            {
                                name = student[2],
                                studentsCount = 1
                            });
                        }
                    }
                    else
                    {
                        Log($"Błąd w danych: {student}");
                    }
                }
                var date = DateTime.UtcNow;
                
                if(format.ToUpper() == "XML")
                {
                    var xml = new XElement("uczelnia",
                    new XAttribute("createdAt", date),
                    new XAttribute("author", "Jakub Adamczyk"),
                    new XElement("studenci"));
                    foreach (var student in students)
                    {
                        xml.Element("studenci").Add(
                            new XElement("student",
                            new XAttribute("index", student.index),
                                    new XElement("fname", student.name),
                                    new XElement("lname", student.sname),
                                    new XElement("birthdate", student.birthday),
                                    new XElement("email", student.email),
                                    new XElement("mothersName", student.mothersName),
                                    new XElement("fathersName", student.fathersName),
                                    new XElement("studies",
                                        new XElement("name", student.studies),
                                        new XElement("mode", student.mode)
                            )));
                    }
                    xml.Add(new XElement("activeStudies"));
                    foreach (var study in studies)
                    {
                        xml.Element("activeStudies").Add(
                            new XElement("studies",
                                new XAttribute("name", study.name),
                                new XAttribute("numberOfStudents", study.studentsCount)
                                ));
                    }

                    xml.Save(output);
                }
                else if(format.ToUpper() == "JSON")
                {
                    var helper = new JsonHelper
                    {
                        author = "Jakub Adamczyk",
                        createdAt = date,
                        students = students.ToList(),
                        studies = studies.ToList()
                    };
                    var json = new JavaScriptSerializer().Serialize(helper);
                    File.WriteAllText(output, json);
                }
                
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                Log($"{e}(Podana ścieżka jest niepoprawna)");
                throw e;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                Log($"{e}(Plik {pathCSV} nie istnieje)");
                throw e;
            }
        }
        public static void Log(string msg)
        {
            using (var writer = new StreamWriter(@"..\..\..\log.txt", true))
            {
                writer.WriteLine(msg);
            }
        }

    }
    class StudentComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            if (x.name == y.name && x.sname == y.sname && x.index == y.index) {
                return false;
            }
            return true;
        }

        public int GetHashCode(Student x)
        {
            return StringComparer.CurrentCultureIgnoreCase.GetHashCode($"{x.name}{x.sname}{x.index}");
        }
    }
    class StudiesComparer : IEqualityComparer<Study>
    {
        public bool Equals(Study x, Study y)
        {
            if (x.name == y.name)
            {
                return false;
            }
            return true;
        }

        public int GetHashCode(Study x)
        {
            return StringComparer.CurrentCultureIgnoreCase.GetHashCode($"{x.name}");
        }
    }
}
class JsonHelper
{
    public string author { get; set; }
    public DateTime createdAt { get; set; }
    public List<zad1.Student> students { get; set; }
    public List<zad1.Study> studies { get; set; }

}
