using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace zad1
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathCSV = args[0] == null ? "data.csv" : args[0];
            var pathXML = args[1] == null ? "result.xml" : args[1];
            var format = args[3] == null ? "XML" : args[3];

            var content = File.ReadLines(pathCSV);
            var date = DateTime.UtcNow;
            var xml = new XElement($"uczelnia createdAt=\"{date}\" author=\"Jakub Adamczyk\"",
                new XElement("studenci", 
                from line in content
                let column = line.Split(',')
                select new XElement("student",
                new XAttribute("index", $"s${line[4]}"),
                new XElement("fname", line[0]),
                new XElement("lname", line[1]),
                new XElement("birthdate", line[5]),
                new XElement("email", line[6]),
                new XElement("mothersName", line[7]),
                new XElement("fathersName", line[8]),
                new XElement("studies", new XElement("name", line[2]), new XElement("mode", line[3]))
                )));
            FileStream writer = new FileStream(pathXML, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>), new XmlRootAttribute("uczelnia"));
            var list = new List<Student>();
            foreach (string line in content){

                var column = line.Split(',');

                list.Add(new Student
                {
                    imie = column[0],
                    nazwisko = column[1],
                    birthday = Convert.ToDateTime(column[5]),
                    email = column[6],
                    mothersName = column[7],
                    fathersName = column[8],
                    studies = column[2],
                    mode = column[3],
                    index = column[4]
                });
            }
            serializer.Serialize(writer, list);

        }
    }
}
