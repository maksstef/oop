using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Products();
            customer.Shop();
            customer.GoShop();
            customer.ToString();
            Console.WriteLine();

            var customer2 = new Table();
            customer2.Shop();
            customer2.GoShop();
            customer2.Repair();
            Console.WriteLine();

            var customer3 = new Monitor();
            customer3.Choose();
            customer3.GoShop();
            Console.WriteLine();

            var customer4 = new Computer();
            customer4.GoShop();
            Console.WriteLine();

            //task 4
            var customer5 = new Headphones();
            customer5.NoChoose();
            ((IEquipment)customer5).NoChoose();
            Console.WriteLine();

            //task 5.1
            //наследственный полиморфизм , тк класс абстрактный ,то 
            //соси будет выполнять абстрактные методы реализованные в Computer
            //type Equipment , methods called from Compucter
            //creating object with help abstract class
            Equipment sos = new Computer();
            sos.Shop();
            sos.Choose();

            //creating object with help interface
            IProducts sos2 = sos;
            sos2.Shop();
            Console.WriteLine();

            //task 5.2
            //is - можно ли преобразовать sos в объект
            Console.WriteLine(sos is object);
            //as - проверяет является ли экземпляр класса Compucter унаследованным от Equipment
            Console.WriteLine(sos as Equipment);
            Console.WriteLine();

            //task 6
            //array abstracts classes
            Equipment[] abstrarr = { new Computer(), new Monitor(), new Headphones(), new Projector(), new Screen() };
            Printer Print = new Printer();

            foreach (var i in abstrarr)
            {
                Print.IAmPrinting(i);
            }
            Console.WriteLine();
            //6.3
            ComputerClass pc = new ComputerClass();
            ComputerClass pc2 = pc;
            pc.Output();
            pc.Push(customer);
            pc.Push(customer2);
            pc.Output();
            Console.WriteLine("Delete element");
            pc.Remove(customer);
            pc.Output();
            Console.WriteLine();
            pc.Push(customer3);
            pc.Output();
            Console.WriteLine("full delete");
            pc.Clear();
            pc.Output();
            Console.WriteLine();

            //6.4
            SchoolEqp eqp1 = new SchoolEqp(100, 5);
            SchoolEqp eqp2 = new SchoolEqp(200, 11);
            SchoolEqp eqp3 = new SchoolEqp(300, 9);
            

            SchoolEqp[] arr = new SchoolEqp[3];
            arr[0] = eqp1;
            arr[1] = eqp2;
            arr[2] = eqp3;

            Controller.PriceSum(arr[0], eqp2, eqp3);
            Console.WriteLine();
            Controller.ReEquip(arr[0], arr[2], eqp3);
            Console.WriteLine();
            Controller.ShowEqpSort(eqp1,arr[1],arr[2]);

            // 7 laba

            //если выполняется условие , то выводится сообщение . В случае невыполнения пропускает дальше.
            //Назначение : аварийный выход с программы.
            int[] aa = null;
            Debug.Assert(aa != null, "Values array cannot be null");

            //7.всё до ассерта
            SchoolEqp eqpex = new SchoolEqp(6100, 11);

            try
            {
                eqpex.CheckPrice();
                eqpex.ChechPrice2();
                eqpex.CheckYear();
            }
            //на основе своих исключений
            catch (ChildException e)
            {
                Console.WriteLine(e.Message);
            }

            catch(ChildException1 e)
            {
                Console.WriteLine(e.Message);
            }

            catch(ChildException2 e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                int a = 1;
                int b = 0;
                int c =  a / b;

                int[] ArrForException = {1,2,3,4};
                int g = ArrForException[7];
            }
            //на основе стандартных исключений , универсальный обработчик
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);//показывает строку , в которой ошибка
            }

            finally
            {
                Console.WriteLine("This is Finally");
            }

            Console.ReadKey();
        }
    }

    

    //6.3
    public class ComputerClass
    {
        private List<Products> data = new List<Products>();
        public static int counter = 0;

        public List<Products> Data { get => data; set => data = value; }
        
        public void Output()
        {
            if(counter == 0)
            {
                Console.WriteLine("empty");
            }
            else
            {
                foreach(object i in Data)
                {
                    Console.WriteLine($" object : {i}");
                }
            }
        }

        public void Remove(Products obj)
        {
            Data.Remove(obj);
        }

        public void Clear()
        {
            if(counter == 0)
            {
                Console.WriteLine("container empty");
            }
            else
            {
                counter = 0;
                Data.Clear();
                Console.WriteLine("container cleaned");
            }
        }

        public void Push(Products obj)
        {
            Data.Add(obj);
            counter++;
        }

    }


        //6.2
    public partial class TypePartial
    {
        private int _num;
        public int Num { get => _num; set => _num = value; }
    }

    public class Products : IProducts
    {
        private Object _varieties;//created field type Object

        public object Varieties { get => _varieties; set => _varieties = value; }//properties


        public override string ToString()
        {
            return "Products";
        }

        public Products()
        {
            _varieties = new Object();
        }

        //virtual methods 
        virtual public void Shop()
        {
            Console.WriteLine("Shop");
        }

        virtual public void GoShop()
        {
            Console.WriteLine("You go to shop");
        }
        //6.1.1
        enum Days : int
        {
            mon = 1,
            tue,
            wen,
            thu,
            fri,
            sat,
            sun
        }
        //6.1.2
        struct Hz
        {
            private string name;
            public string Name { get => name; set => name = value; }
        }
    }
    //interface + abstract class(4 task)
    abstract class Equipment : Products, IEquipment
    {
        public override string ToString()
        {
            return "Equipment";
        }

        virtual public void Choose()
        {
            Console.WriteLine("Your choice is ...");
        }

        virtual public void NoChoose()
        {
            Console.WriteLine("You no choosed ...");
        }
    }

    class Monitor : Equipment
    {
        public override string ToString()
        {
            return "Monitor";
        }

        override public void Choose()
        {
            Console.WriteLine("Your choice is Monitor");
        }
    }

    class Computer : Equipment, IOtherEquipment
    {
        public override string ToString()
        {
            return "Computer";
        }

        public override void Choose()
        {
            base.Choose();
            Console.WriteLine("erat");
        }

        public override void Shop()
        {
            Console.WriteLine("shop comp");
        }

        public void Repair()
        {
            Console.WriteLine("we are will repair your computer");
        }
    }
    //4 task , second part
    class Headphones : Equipment, IEquipment
    {
        public override string ToString()
        {
            return "Headphones";
        }

        public override void NoChoose()//implicit realisation
        {
            Console.WriteLine("You no choosed headphones");
        }

        void IEquipment.NoChoose() { Console.WriteLine("You no ..."); } //explicit realisation

    }

    class Projector : Equipment
    {
        public override string ToString()
        {
            return "Projector";
        }
    }
    //3 task
    sealed class Table : Products, IOtherEquipment
    {
        public override string ToString()
        {
            return "Table";
        }

        override public void Shop()
        {
            Console.WriteLine("Table");
        }

        override public void GoShop()
        {
            Console.WriteLine("You go to table");
        }

        public void Repair()
        {
            Console.WriteLine("Your chair is broke");
        }
    }

    class Screen : Equipment
    {
        public override string ToString()
        {
            return "Screen";
        }
    }

    //передаем классы , которые наследуются от Equipment и выводим метод ToString()
    class Printer
    {
        public void IAmPrinting(Equipment obj)
        {
            Console.WriteLine(obj.ToString());
        }
    }

    //6.4

    //7
    public class SchoolEqp
    {
        private int price;
        private int year;

        public int Price { get => price; set => price = value; }
        public int Year { get => year; set => year = value; }

        public SchoolEqp(int price, int year)
        {
            this.Price = price;
            this.Year = year;
        }

        public void CheckPrice()
        {
            if (Price > 500) { throw new ChildException("цена больше 500!"); }
        }

        public void ChechPrice2()
        {
            if (Price < 200) { throw new ChildException1("Цена меньше 200!"); }
        }

        public void CheckYear()
        {
            if (Year > 10) { throw new ChildException2("Год больше 10!"); }
        }
    }

    public class Controller
    {
       static public void PriceSum(SchoolEqp obj,SchoolEqp obj2,SchoolEqp obj3)
        {
            Console.WriteLine($"sum is {obj.Price+obj2.Price+obj3.Price}");
        }

        static public void ReEquip(SchoolEqp obj, SchoolEqp obj2, SchoolEqp obj3)
        {
            int maxyear = 6;
            SchoolEqp[] arr = new SchoolEqp[3];
            arr[0] = obj;
            arr[1] = obj2;
            arr[2] = obj3;
            for(int i = 0; i < arr.Length; ++i)
            {
                if(arr[i].Year > maxyear)
                {
                    Console.WriteLine($"equipment {i+1} need to destroy");
                }
                else
                {
                    Console.WriteLine($"equipment {i+1} in normal condition");
                }
            }
        }

        static public void ShowEqpSort(SchoolEqp obj, SchoolEqp obj2, SchoolEqp obj3)
        {
            SchoolEqp[] arr = new SchoolEqp[3];
            arr[0] = obj;
            arr[1] = obj2;
            arr[2] = obj3;
            int flex;
            for(int i = 0; i < arr.Length-1; ++i)
            {
                for(int j = i + 1; j < arr.Length; ++j)
                {
                    if(arr[i].Price > arr[j].Price)
                    {
                        flex = arr[i].Price;
                        arr[i].Price = arr[j].Price;
                        arr[j].Price = flex;
                    }
                }
            }
            for(int  i =0;i < arr.Length;++i)
            {
                Console.WriteLine(arr[i].Price);
            }
            
        }
    }
}
