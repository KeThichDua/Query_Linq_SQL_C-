using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace btLinq
{
    class request2
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
                Console.WriteLine("Truy van 2: ");
                foreach(var item in classes)
                {
                    int count = (from s in students where s.classId == item.Idc select s).Count();
                    Console.WriteLine(item.Name + ", " + count);
                }

                // cách 2
                Console.WriteLine("Cách 2");
                var std2 = classes.GroupJoin(
                    students,
                    c => c.Idc,
                    s => s.classId,
                    (c, s) => new
                    {
                        id = c.Name,
                        count = s.Count()
                    }
                    );

                foreach (var item in std2)
                {
                    Console.WriteLine(item.id + ", " + item.count);
                }

                // cách 3
                Console.WriteLine("Cách 3");
                foreach (var item in classes)
                {
                    Console.WriteLine(item.Name + ", " + item.students.Count());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi truy van! ", ex);
            }
        }
    }
}
