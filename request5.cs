using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace btLinq
{
    class request5
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

//select class_name, min(fil.Name) as name_of_first_student
//from
//(select c.Name as class_name, s.Name
//from class c left join student s   on s.classId = c.Id) fil
//group by class_name

                // cách 1
                Console.WriteLine("Truy van 5: ");
                var stds =
                   from c in classes
                   join s in students
                   on c.Idc equals s.classId into gr
                   from g in gr.DefaultIfEmpty()
                   select new
                   {
                       clName = c.Name,
                       stName = g.classId == c.Idc ? g.Name : String.Empty
                   };
                var std =
                    from s in stds
                    group s by s.clName into g
                    select new
                    {
                        clName = g.Key,
                        fisrt = g.First().stName
                    };
                foreach (var item in std)
                {
                    Console.WriteLine(item.clName + ", " + item.fisrt);
                }

                // cách 2
                Console.WriteLine("Cách 2");
                var stds2 = classes.GroupJoin(
                    students, 
                    c => c.Idc,
                    s => s.classId,
                    (c,s) => new
                    {
                        clname = c.Name,
                        fir = s.First().Name
                    });
                foreach (var item in stds2)
                {

                    Console.WriteLine(item.clname + ", " + item.fir);
                }

                // cách 3
                Console.WriteLine("Cách 3: ");
                foreach(var item in classes)
                {
                    student st = new student();
                    if (item.students.Count != 0)
                    {
                        st = item.students.First();
                    }
                    Console.WriteLine(item.Name + ", " + st.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi truy van!", ex);
            }
        }
    }
}
