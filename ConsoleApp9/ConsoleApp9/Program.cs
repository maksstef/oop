using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            //1
            Console.WriteLine("----------1 task---------- ");
            ArrayList list = new ArrayList() { 1, 2, 3, 4, 5 };

            list.Add("string");
            list.Add(new Student());
            list.Remove(4);
            list.RemoveAt(0);

            Console.WriteLine("List count : {0}", list.Count);

            foreach (object o in list)
            {
                Console.WriteLine(o);
            }

            Console.WriteLine("Search : " + list.BinarySearch(5));

            Console.WriteLine();

            //2
            Console.WriteLine("----------2 task----------");
            Dictionary<int, string> values = new Dictionary<int, string>
            {
                { 1,"Earth is round"},
                { 2,"Earth is flat"},
            };

            foreach (var i in values)
            {
                Console.WriteLine("{0} - {1}", i.Key, i.Value);
            }

            values.Remove(1);
            values.Add(1, "truth");
            values[3] = "truth 2";
            Console.WriteLine("-----after add/del : -----");
            foreach (var i in values)
            {
                Console.WriteLine("{0} - {1}", i.Key, i.Value);
            }

            List<string> values2 = new List<string>();

            foreach (KeyValuePair<int, string> element in values)
            {
                values2.Add(element.Value);
            }

            Console.WriteLine();

            foreach (string s in values2)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine();
            Console.WriteLine(values2.Contains("truth"));
            Console.WriteLine();

            /* task fo test
            Console.WriteLine("enter array");
            Random ran = new Random();
            int[,] arr = new int[3,3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arr[i, j] = ran.Next(1, 10);
                    Console.Write("{0}\t",arr[i,j]);
                }
                Console.WriteLine();
            }
            */

            //3
            Console.WriteLine("----------task 3----------");

            Products prod = new Products("hell");
            Products prod1 = new Products("hell2");
            Products prod2 = new Products("hell3");
            Products prod3 = new Products("hell4");


            Dictionary<int, Products> values3 = new Dictionary<int, Products>
            {
                { 1, prod},
                { 2, prod1},
                { 3, prod2}
            };

            foreach (var i in values3)
            {
                Console.WriteLine(i.Key + " : " + i.Value);
            }

            Console.WriteLine();
            values3.Remove(2);
            values3.Add(2, prod3);
            values3[4] = prod1;

            Console.WriteLine("-----after add/del-----");
            foreach (var i in values3)
            {
                Console.WriteLine(i.Key + " " + i.Value);
            }

            List<Products> values4 = new List<Products>();

            foreach (KeyValuePair<int, Products> elem in values3)
            {
                values4.Add(elem.Value);
            }

            Console.WriteLine("-----contains in list : -----");

            foreach (var i in values4)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine(values4.Contains(prod));

            //realisation IComparable

            Equipment eqp = new Equipment("hell666");

            Products[] arrprod = {prod3,eqp,prod,prod2};

            Console.WriteLine("-----before IComparable-----");

            foreach(Products p in arrprod)
            {
                Console.WriteLine(p.str);
            }

            Array.Sort(arrprod);

            Console.WriteLine("-----after IComparable-----");

            foreach(Products p in arrprod)
            {
                Console.WriteLine(p.str);
            }

            Console.WriteLine();

            //4
            Console.WriteLine("----------task 4----------");

            ObservableCollection<Student> students = new ObservableCollection<Student>
            {
                new Student{Name = "Petya"},
                new Student{Name = "Jorge"},
                new Student{Name = "Mark"}
            };

            students.CollectionChanged += Students_CollectionChanged;

            students.Add(new Student { Name = "Alica" });
            students.RemoveAt(1);
            students[0] = new Student { Name = "Luk" };

            foreach(Student std in students)
            {
                Console.WriteLine(std.Name);
            }

            Console.ReadLine();
        }

        private static void Students_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Student newStud = e.NewItems[0] as Student;
                    Console.WriteLine("Add new object : {0}",newStud.Name);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Student oldStud = e.OldItems[0] as Student;
                    Console.WriteLine("Deleted object : {0}",oldStud.Name);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Student replasedStud = e.OldItems[0] as Student;
                    Student replacingStud = e.NewItems[0] as Student;
                    Console.WriteLine("Object {0} replaced object {1}",replasedStud.Name,replacingStud.Name);
                    break;
            }
        }
    }

    class Student 
    {
        public string Name { get; set; }
    }

    class Products : IComparable
    {
        public string str;
        public Products(string str)
        {
            this.str = str;
        }

        public int CompareTo(object o)
        {
            Products p = o as Products;
            if (p != null)
                return this.str.CompareTo(p.str);
            else
                throw new Exception("impossible to compare these objects");
        }

    }

    class Equipment : Products
    {
        public Equipment(string str):base(str)
        {

        }
    }
}
