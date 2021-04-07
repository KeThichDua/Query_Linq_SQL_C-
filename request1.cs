using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace btLinq
{
    class request1
    {
        public void Run(string connString)
        {
            try
            {
                // Create data context  
                DataContext db = new DataContext(connString);
                // Create typed table  

                Table<student> students = db.GetTable<student>();
                Table<class1> classes = db.GetTable<class1>();

                // cách 1
                Console.WriteLine("Truy van 1: ");
                var stdDetails =
                   from s in students
                   from c in classes 
                   select new
                   {
                       studentId = s.Id,
                       classId = c.Idc
                   };
                foreach (var item in stdDetails)
                {
                    Console.WriteLine(item.studentId + ", " + item.classId);
                }

                // cách 2
                Console.WriteLine("Cách 2");
                var stdDetails2 = students.SelectMany(
                    c => classes,
                    (s,c)   => new
                    {
                        s.Id ,
                        c.Idc
                    });
                foreach (var item in stdDetails2)
                {
                    
                    Console.WriteLine(item.Id + ", " + item.Idc);
                }

                // cách 3
                Console.WriteLine("Cách 3");
                foreach(var item in classes)
                {
                    foreach(var items in students)
                    {
                        Console.WriteLine(items.Id + ", " + item.Idc);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi truy van!", ex);
            }
        }
    }
}
