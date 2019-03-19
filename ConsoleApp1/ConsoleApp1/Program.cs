using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {



            /*
                  Func<int[], string, Tuple<int, int, int, char>> 
                  fun = (arr, str) => arr.Aggregate(Tuple.Create(Int32.MinValue, Int32.MaxValue, 0, str[0]), (y, x) => Tuple.Create(Math.Max(y.Item1, x), Math.Min(y.Item2, x), y.Item3 + x, y.Item4));
                  Console.WriteLine(fun(new int[] { 1, -2, 3, -4, 5, -6, 7, -8, 9, 0 }, "Abracadabra"));
           */


            Console.WriteLine("1 - types , 2 - strings , 3 - arrays , 4 - tuples , 5 - Fifth task");
            string select = Console.ReadLine();
            switch (select)
            {
                
                case "1": Work.Types();
                break;
                case "2": Work.Strings();
                break;
                case "3": Work.Arrays();
                break;
                case "4": Work.Tuples();
                break;
                case "5": Work.Fifth();
                break;
                default:Console.WriteLine("mistake!");
                break;
             }
            Console.ReadKey();
        }
    }

    class Work
    {
        public static void Types()
        {
            byte a1 = 4;
            short a2 = 5;
            ushort a3 = 3;
            int a4 = 4;
            uint a5 = 5;
            long a6 = 6;
            ulong a7 = 7;
            float a8 = 7.6F;
            double a9 = 5.5;
            char a10 = 'a';
            bool a11 = true;
            decimal a12 = 100;
            string a13 = "a13";
            object a14 = 145;

            //неявное преобразование
            short b1 = a1;
            int b2 = a1;
            long b3 = a1;
            long b4 = a4;
            double b5 = a8;

            //явное преобразование 
            byte c1 = (byte)a2;
            byte c2 = (byte)a3;
            float c3 = (float)a9;
            int c4 = (int)a14;
            long c5 = (long)a12;

            //упаковка и распаковка
            int val = 5;
            object obj = val;
            int Unbox = (int)obj;

            //работа с неявно типизированное переменной
            var mas = new[] { 2, 4, 6, 8 };
            Console.WriteLine(mas.GetType());
            var mas1 = new[] { 3.6, 1.45 };
            Console.WriteLine(mas1.GetType());

            //work with Nullable
            int? x = null;
            int y = x ?? 1;
            Console.WriteLine(y);
        }

        public static void Strings()
        {
            //сравнение строковых литералов
            string path1 = "ccc";
            string path2 = "qqq";
            String.Compare(path1,path2);

            string str1 = "ttt";
            string str2 = "yyy";
            string str3 = "xrr";
            
            string str4 = str1 + str2;
            String.Copy(str1);

            //выделение подстроки
            str2.Substring(2,1);

            //вставка подстроки
            string str5 = str2.Insert(2,"-");
            Console.WriteLine(str5);

            //удаление подстроки 
            str3 = str3.Remove(1,2);
            Console.WriteLine(str3);

            string empty = "";
            string khekhe = null;
            string ooo = empty + khekhe;
            bool n = empty == khekhe;

            //srtringbuilder
            StringBuilder newstr = new StringBuilder("maksim", 20);
            newstr = newstr.Remove(1, 2);
            newstr.Append("s");
            newstr.Insert(1, "-");
            Console.WriteLine(newstr);
        }

        public static void Arrays()
        {
            int[,] mas = { { 1, 2, 3 }, { 4, 5, 6 } };
            int rows = mas.GetUpperBound(0) + 1;//get the numbers of strings(rows)
            int columns = mas.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine("\t");
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($" {mas[i, j]} ");
                }
            }

            Console.WriteLine("\t");

            foreach (int i in mas)
            {
                Console.WriteLine($"{i} \t");
            }

            //task "b" in array
            Console.WriteLine("\t");
            string[] masstr = { "yes", "no" ,"hmm"};
            foreach (string i in masstr)
            {
                Console.WriteLine($"{i} ");
            }
            Console.WriteLine(masstr.Length);
            string number = Console.ReadLine();
            float trueword = Convert.ToSingle(number);
            int num = (int)trueword;
            string word = Console.ReadLine();

            for (int i = 0; i < masstr.Length; i++)
            {
                if (i == num)//если i = введенной цифре , то мы присваиваем элементу в массиве под этим номером word
                {
                    masstr[i] = word;
                }
            }
            foreach (string i in masstr)
            {
                Console.WriteLine($"{i} ");
            }
            Console.WriteLine();

            //task "c" in array

            int[][] myArr = new int[4][];
            myArr[0] = new int[2];
            myArr[1] = new int[3];
            myArr[2] = new int[4];

            for (int i = 0; i < 2; i++)
            {
                myArr[0][i] = i;
            }
            for (int i = 0; i < 3; i++)
            {
                myArr[1][i] = i;
            }
            for (int i = 0; i < 4; i++)
            {
                myArr[2][i] = i;
            }

            foreach (int i in myArr[0])
            {
                Console.WriteLine(myArr[0][i] + " ");
            }
            Console.WriteLine();
            foreach (int i in myArr[1])
            {
                Console.WriteLine(myArr[1][i] + " ");
            }
            Console.WriteLine();
            foreach (int i in myArr[2])
            {
                Console.WriteLine(myArr[2][i] + " ");
            }
            Console.WriteLine();

            //task d in array
            var difarr = new[] { 1, 2, '3', 4 };
            for (var i = 0; i < 5;i++)
                Console.Write(difarr[i] + " ");
            Console.WriteLine();
        }

        public static void Tuples()
        {
            Console.WriteLine();
            (int n1, string n2, char n3, string n4, ulong n5) student = (1, "max", 'm', "fit", 9991999);
            Console.WriteLine(student.Item1);
            Console.WriteLine(student.Item2);
            Console.WriteLine(student.Item3);
            Console.WriteLine(student.Item4);
            Console.WriteLine(student.Item5);
            Console.WriteLine();
            Console.WriteLine(student.Item1);
            Console.WriteLine(student.Item3);
            Console.WriteLine(student.Item4);
            Console.WriteLine();
            var Base1 = student.Item1;
            var Base2 = student.Item2;
            var Base3 = student.Item3;
            var Base4 = student.Item4;
            var Base5 = student.Item5;

            (int m1, string m2, char m3, string m4, ulong m5) student2 = (2, "anna", 'w', "htit", 1234324);

            Console.WriteLine(Object.Equals(student,student2));
        }

        public static void Fifth()
        {
            (int, int, int, char) MainFunction(int[] arrParam, string strParam)

            {
                return (arrParam.Max(), arrParam.Min(), arrParam.Sum(), strParam.First());
            }

            Console.WriteLine("Write int number");

            int[] Task = new int[4];

            for (int i = 0; i < Task.Length; i++)

            {
                Console.WriteLine($"Task[{i}]");
                Task[i] = Int32.Parse(Console.ReadLine());
            }

            Console.WriteLine("Write string");
            string Task2 = Console.ReadLine();
            Console.WriteLine(MainFunction(Task, Task2));
            Console.ReadKey();
        }
    }
}
