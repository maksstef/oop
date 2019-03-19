using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new Set();
            obj = obj << 1;
            obj.Collection.Add(5);
            obj = obj << 4;


            obj.ShowData();
            obj = obj >> 1;
            Console.WriteLine();
            obj.ShowData();


            var anotherObj = new Set();
            anotherObj = anotherObj << 2;
            anotherObj = anotherObj << 4;

            Console.WriteLine(anotherObj>2);

            Console.WriteLine(obj != anotherObj);

            var result = obj % anotherObj;


            obj.FindMin();


            obj = obj << 15;
            obj = obj << 20;
            obj = obj << 9;
            obj = obj << 0;

            obj.ShowData();
            obj.SortBy();
            Console.WriteLine();
            Console.WriteLine();
            obj.ShowData();


            var date = new Set.Date();
            Console.WriteLine(date.RecentDate);

            var own1 = new Owner(1, "max", "erat");
            Console.WriteLine($"{own1.Id} {own1.Name} {own1.Organisation}");

            MathOperation.FindMax(obj);
            MathOperation.FindMin(obj);
            MathOperation.FindCount(obj);
            MathOperation.Extension(obj, 20);
        }
    }
}
