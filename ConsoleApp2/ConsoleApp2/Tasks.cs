using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Tasks
    {
        public readonly double kie = 23;
        public const int kie2 = 24;

        private Tasks(int z = 5)
        {

        }
        //ref 
        public static void TaskRef()
        {
            int param = 10;
            testMethod(ref param);
            Console.WriteLine("TaskRef = {0}", param);
            Console.ReadLine();
        }
        public static void testMethod(ref int param)
        {
            param++;
        }


        //out 
        public static void TaskOut()
        {
            int param;
            testMethod2(out param);
            Console.WriteLine("TaskOut = {0}", param);
            Console.ReadLine();
        }

        static void testMethod2(out int param)
        {
            param = 10;
            param++;
        }


    }

    //get set
    public class Person
    {
        private int age;

        public int Age
        {
            set
            {
                if (value < 18)
                {
                    Console.WriteLine("Возраст должен быть больше 17");
                }
                else
                {
                    age = value;
                }
            }
            get { return age; }
        }
    }


    //stat field
    public class Room
    {
        // Если мы напишем вот так: "public int count;", то данное поле
        // будет у каждого объекта, и у каждого объекта оно будет своё.
        // Причем, если не создано ни одного объекта, то это поле не будет существовать вообще
        // Поэтому делаем это поле статическим
        public static int count;
        // Создадим конструктор, в котором будем увеличить count при создании объекта
        public Room()
        {
            count++;
        }
        // Также напишем статический метод, который выводит количество созданных объектов
        public static void ShowNumberOfObjects()
        {
            Console.WriteLine(Room.count.ToString());
        }
    }

    public class Program1
    {
        public static void Main1()
        {
            // Давайте посмотрим чему равно count без создания объектов
            Room.ShowNumberOfObjects(); // выйдет 0, т.к. мы пока не создали ни одного объекта
                                        // Создадим 3 комнаты
            Room room1 = new Room();
            Room room2 = new Room();
            Room room3 = new Room();
            Room.ShowNumberOfObjects(); // выйдет 3
            Console.ReadLine();
        }
    }


    //partial class - для работы нескольких прогеров над одним проектом
    public partial class Employee
    {
        public void DoWork()
        {
        }
    }

    public partial class Employee
    {
        public void ExitWork()
        {
        }
    }
}

