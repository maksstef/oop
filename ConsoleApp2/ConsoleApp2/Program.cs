using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Bus
    {
        public string sname;
        public int nbus;
        public int nroute;
        public string mark;
        public int year;
        public int mileage;
        public int ID;

        public Bus(string sname, int nbus, int nroute)
        {
            this.sname = sname;
            this.nbus = nbus;
            this.nroute = nroute;
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
            ID = (int) sname.GetHashCode();
            return ID;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Bus bus1 = new Bus("Nikiforov L.L.", 6794, 3, "MAZ", 2010, 110000);
            Bus bus2 = new Bus("Petrov V.V.", 1234, 2, "YAZ", 1975, 27009);
            Bus bus3 = new Bus("Bobov P.P.", 6801, 1, "VAZ", 2000, 64000);
            Bus[] arrobj = new Bus[3];
            arrobj[0] = bus1;
            arrobj[1] = bus2;
            arrobj[2] = bus3;

            Console.WriteLine(bus1.IDHash());


            Console.WriteLine("Enter age bus");
            int Age = Int32.Parse(Console.ReadLine());
            Console.WriteLine();
            for (int i = 0; i < arrobj.Length; ++i)
            {
                if (arrobj[i].Busyear() > Age)
                {
                    Console.WriteLine(arrobj[i].nbus);
                    Console.WriteLine(arrobj[i].mark);
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Enter route number");
            int RouteNum = Int32.Parse(Console.ReadLine());
            Console.WriteLine();
            for (int i = 0; i < arrobj.Length; ++i)
            {
                if(arrobj[i].nroute == RouteNum)
                {
                    Console.WriteLine(arrobj[i].sname);
                    Console.WriteLine(arrobj[i].nbus);
                }
            }

         

            Person tom = new  Person();
            tom.Age = 17;

            Tasks.TaskRef();
            Tasks.TaskOut();
        

            Program1.Main1();
            Console.WriteLine();
            Console.ReadKey();
        }
       
    }

}