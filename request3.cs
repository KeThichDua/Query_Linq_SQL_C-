using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace btLinq
{
    class request3
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
                Console.WriteLine("Truy van 3: ");
                var std =
                    from s in students
                    group s by s.classId into g
                    select new
                    {
                        id = g.Key,
                        stds = g.ToList()
                    };
                foreach(var item in std)
                {
                    Console.WriteLine(item.id + ", " + item.stds.Count());
                }

                // cách 2
                Console.WriteLine("Cách 2");
                var std2 = students.GroupBy(
                    s => s.classId,
                    s => s,
                    (key, g) => new
                    {
                        id = key,
                        stds = g.ToList()
                    }
                    );
                foreach (var item in std2)
                {
                    Console.WriteLine(item.id + ", " + item.stds.Count());
                }

                // cách 3
                Console.WriteLine("Cách 3");
                foreach(var item in classes)
                {
                    if(item.students.Count() == 0)
                    {
                        continue;
                    }
                    Console.WriteLine(item.Idc + ", " + item.students.Count());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi truy van! ", ex);
            }
        }
    }
}
