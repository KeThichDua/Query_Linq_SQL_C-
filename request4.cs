using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace btLinq
{
    class request4
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
                Console.WriteLine("Truy van 4: ");
                var std =
                    from s in students
                    from c in classes
                    where c.Idc == s.classId
                    group s by c.Name into g
                    select new
                    {
                        id = g.Key,
                        stds = g.ToList()
                    };
                foreach (var item in std)
                {
                    Console.WriteLine(item.id + ", " + item.stds.Count());
                }

                // cách 2
                Console.WriteLine("Cách 2");
                var std3 = students.SelectMany(
                    c => classes,
                    (s, c) => new
                    {
                        s.Id,
                        s.classId,
                        c.Name,
                        c.Idc
                    })
                    .Where(s => s.classId == s.Idc)
                    .GroupBy(
                    c => c.Name,
                    c => c.Id,
                    (key, g) =>new
                    {
                        id = key,
                        count = g.Count()
                    }
                    );
                foreach (var item in std3)
                {
                    Console.WriteLine(item.id + ", " + item.count);
                }

                //cách 3
                Console.WriteLine("Cách 3");
                foreach (var item in classes)
                {
                    if (item.students.Count() == 0)
                    {
                        continue;
                    }
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
