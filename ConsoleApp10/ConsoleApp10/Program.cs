using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("-----1.1-----");
            string[] months = {"January","February","March","April","May",
                "June","July","August","September","October","November","December"};

            Console.Write("Enter lenght of string : ");
            int n = int.Parse(Console.ReadLine());

            var request_1_1 = from m in months
                              where m.Length == n
                              orderby m
                              select m;

            foreach(string s in request_1_1)
                Console.WriteLine(s);
            Console.WriteLine();

            string[] wintermonths = { "January", "February", "December" };
            string[] summermonths = { "June", "July", "August" };

            Console.WriteLine("-----1.2-----");
            var request_1_2 = from m in months
                              where m == wintermonths[0] || m == wintermonths[1] ||
                              m == wintermonths[2] || m == summermonths[0] ||
                              m == summermonths[1] || m == summermonths[2]
                              orderby m
                              select m;

            foreach(string s in request_1_2)
                Console.WriteLine(s);
            Console.WriteLine();


            Console.WriteLine("-----1.3-----");
            var request_1_3 = from m in months
                              orderby m
                              select m;

            foreach(string s in request_1_3)
                Console.WriteLine(s);
            Console.WriteLine();


            Console.WriteLine("----1.4-----");
            var request_1_4 = from m in months
                              where m.Contains('u') && m.Length >= 4
                              orderby m
                              select m;

            foreach(string s in request_1_4)
                Console.WriteLine(s);
            Console.WriteLine();


            //2
            Bus bus1 = new Bus("nikiforov",1452, 1,10,10000);
            Bus bus2 = new Bus("petrov", 3705, 5,20,10001);
            Bus bus3 = new Bus("lobkov", 5493, 119,15,15000);
            
            List<Bus> buses = new List<Bus>() { bus1, bus2, bus3 };

            //3
            Console.WriteLine("-----3-----");
            Console.Write("enter number of bus : ");
            int num = int.Parse(Console.ReadLine());

            var busrequest_1 = from b in buses
                               where b.nroute == num
                               orderby b
                               select b;

            foreach(var i in busrequest_1)
                Console.WriteLine(i.nbus+", "+i.sname+", number of route :"+i.nroute);
            Console.WriteLine();


            Console.Write("enter age : ");
            int num2 = int.Parse(Console.ReadLine());

            var busrequest_2 = from b in buses
                               where b.year > num2
                               orderby b
                               select b;

            foreach(var i in busrequest_2)
                Console.WriteLine(i.nbus + ", " + i.sname + ", number of route :" + i.nroute);
            Console.WriteLine();


            var busrequest_3 = from b in buses
                               orderby b.mileage
                               select "mileage : "+b.mileage+", number of bus : "+b.nbus;

            Console.WriteLine(busrequest_3.Min());
            Console.WriteLine();


            var busrequest_4 = from b in buses
                               orderby b.mileage
                               select "mileage : " + b.mileage + ", number of bus : " + b.nbus;

            Console.WriteLine(busrequest_4.Max());
            Console.WriteLine();


            var busrequest_5 = from b in buses
                               orderby b.nbus
                               select b;

            foreach (var i in busrequest_5)
                Console.WriteLine(i.nbus + ", " + i.sname + ", number of route :" + i.nroute);
            Console.WriteLine();

            //4
            Console.WriteLine("-----4-----");
            var myrequest = from b in buses
                            where b.sname != "Petro" || b.nbus != 666
                            orderby b descending
                            select b;

            foreach(var i in myrequest)
                Console.WriteLine(i.sname);

            //5
            Console.WriteLine("-----5-----");
            List<Car> cars = new List<Car>
            { 
                new Car {name = "mazda", numberr = 1},
                new Car {name = "Folkswagen", numberr = 5}
            };

            var myrequest2 = from b in buses
                             join b2 in cars on b.nroute equals b2.numberr
                             select new { Name = b2.name, Numb = b.nroute, Sname = b.sname };

            foreach(var i in myrequest2)
                Console.WriteLine("{0} - {1} , {2}",i.Name,i.Numb,i.Sname);


        }
    }

    //into - хранение временного результата group, join или select в новый идентификатор
    //descending используется в предложении orderby в выражении запроса, задавая порядок сортировки от наибольшего значения к наименьшему.
    //ascending по умолчанию
    //
    public class Car
    {
        public string name;
        public int numberr;

    }

    public class Bus : IComparable , IComparer<Bus>
    {
        public string sname;
        public int nbus;
        public int nroute;
        public string mark;
        public int year;
        public int mileage;
        public int ID;

        public Bus(string sname, int nbus, int nroute, int year,int mileage)
        {
            this.sname = sname;
            this.nbus = nbus;
            this.nroute = nroute;
            this.year = year;
            this.mileage = mileage;
        }

        public Bus()
        {

        }

        static Bus()
        {

        }

        public Bus(string sname, int nbus, int nroute, string mark, int year, int mileage)
        {
            this.sname = sname;
            this.nbus = nbus;
            this.nroute = nroute;
            this.mark = mark;
            this.year = year;
            this.mileage = mileage;
        }

        public int Busyear()
        {
            int Current_Year = DateTime.Now.Year;
            int busage = Current_Year - year;
            return busage;
        }

        public int IDHash()
        {
            ID = (int)sname.GetHashCode();
            return ID;
        }

        int IComparable.CompareTo(object obj)
        {
            Bus c = (Bus)obj;
            return String.Compare(this.sname, c.sname);
        }

        /*int IComparer.Compare(object a, object b)
        {
            Bus c1 = (Bus)a;
            Bus c2 = (Bus)b;
            if (c1.year > c2.year)
                return 1;
            if (c1.year < c2.year)
                return -1;
            else
                return 0;
        }

        public static IComparer sortYearAscending()
        {
            return (IComparer)new sortYearAscendingHelper();
        }*/
        
        public int Compare(Bus x, Bus y)
        {
            // TODO: Handle x or y being null, or them not having names
            return x.nroute.CompareTo(y.nroute);
        }

    }
    
}
