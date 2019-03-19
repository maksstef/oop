using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
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

                foreach(var i in abstrarr)
                {
                    Print.IAmPrinting(i);
                }

                Console.ReadKey();
            }
        }
    
    
    public class Products : IProducts
    {
        private Object _varieties;//created field type Object

        public object Varieties { get => _varieties; set => _varieties = value; }//properties

        public override string ToString()
        {
            return "Products";
        }

        public Products ()
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

    class Computer : Equipment , IOtherEquipment
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
    class Headphones : Equipment , IEquipment
    {
        public override string ToString()
        {
            return "Headphones";
        }

        public override void NoChoose()//implicit realisation
        {
            Console.WriteLine("You no choosed headphones");
        }

        void IEquipment.NoChoose(){ Console.WriteLine("You no ..."); } //explicit realisation
       
    }

    class Projector : Equipment
    {
        public override string ToString()
        {
            return "Projector";
        }
    }
    //3 task
    sealed class Table : Products , IOtherEquipment
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
}
