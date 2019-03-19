using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp13
{
    class Program
    {
        static void Main(string[] args)
        {
            /*int x = Int32.Parse(Console.ReadLine());
            Console.WriteLine(x.GetType());*/

            //1.1
            ComputerClass cs1 = new ComputerClass();
            ComputerClass cs2 = new ComputerClass();
            Person p = new Person("soap", 98);
            Person pxml = new Person("xml_eee", 54);
            Person pjson = new Person("json_ooo", 098);

            BinaryFormatter fm = new BinaryFormatter();

            using(FileStream fs = new FileStream(@"C:\temp\people.dat",FileMode.OpenOrCreate))
            {
                fm.Serialize(fs, cs1);
                Console.WriteLine("Serialization complete by binary");
            }

            using (FileStream fs = new FileStream(@"C:\temp\people.dat", FileMode.OpenOrCreate))
            {
                ComputerClass new_cs1 = (ComputerClass)fm.Deserialize(fs);
                Console.WriteLine("Deserialization complete");
                Console.WriteLine(new_cs1.Data);
            }
            Console.WriteLine();

            //1.2
            SoapFormatter fm2 = new SoapFormatter();

            using (FileStream fs = new FileStream(@"C:\temp\people.soap", FileMode.OpenOrCreate))
            {
                fm2.Serialize(fs, p);
                Console.WriteLine("Serialization complete by SOAP");
            }

            using (FileStream fs = new FileStream(@"C:\temp\people.soap", FileMode.OpenOrCreate))
            {
                Person new_cs2 = (Person)fm2.Deserialize(fs);
                Console.WriteLine("Deserialization complete by SOAP");
                Console.WriteLine(new_cs2.Name+" "+new_cs2.Year);
            }
            Console.WriteLine();

            //1.3
            DataContractJsonSerializer jfm = new DataContractJsonSerializer(typeof(Person));

            using (FileStream fs = new FileStream(@"C:\temp\people.json",FileMode.OpenOrCreate))
            {
                jfm.WriteObject(fs, pjson);
                Console.WriteLine("Serialization complete by Json");
            }

            using(FileStream fs = new FileStream(@"C:\temp\people.json", FileMode.OpenOrCreate))
            {
                Person newp = (Person)jfm.ReadObject(fs);
                Console.WriteLine("Deserialization complete by Json");
                Console.WriteLine($"{newp.Name}  {newp.Year}");
            }
            Console.WriteLine();


            //1.4
            XmlSerializer fm4 = new XmlSerializer(typeof(Person));

            using(FileStream fs = new FileStream(@"C:\temp\people.xml", FileMode.OpenOrCreate))
            {
                fm4.Serialize(fs, pxml);
                Console.WriteLine("Serialization complete by XML");
            }

            using (FileStream fs = new FileStream(@"C:\temp\people.xml", FileMode.OpenOrCreate))
            {
                Person newp = (Person)fm4.Deserialize(fs);
                Console.WriteLine("Deserialization complete by XML");
                Console.WriteLine(newp.Name+" "+newp.Year);
            }
            Console.WriteLine();

            //2
            Person person = new Person("Nil", 20);
            Person person2 = new Person("Iren", 22);
            Person person3 = new Person("Zack", 19);

            Person[] pp = { person, person2 ,person3};

            BinaryFormatter formatter = new BinaryFormatter();

            //serialization
            using (FileStream fs = new FileStream(@"C:\temp\people.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, pp);
                Console.WriteLine("object serialized");
            }

            //deserialization
            using (FileStream fs = new FileStream(@"C:\temp\people.dat", FileMode.OpenOrCreate))
            {
                Person[] despp = (Person[])formatter.Deserialize(fs);
                Console.WriteLine("Deserialization :");
                foreach (Person pn in despp)
                {
                    Console.WriteLine("Name : {0} , age : {1}", pn.Name, pn.Year);
                }
            }
            Console.WriteLine();

            //3
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(@"C:\temp\people.xml");
            XmlElement xroot = xdoc.DocumentElement;

            XmlNodeList childnodes = xroot.SelectNodes("Name");
            foreach(XmlNode n in childnodes)
            {
                Console.WriteLine(n.OuterXml);
            }

            XmlNodeList childnodes2 = xroot.SelectNodes("Year");
            foreach (XmlNode n in childnodes2)
            {
                Console.WriteLine(n.OuterXml);
            }
            Console.WriteLine();

            //4
            XDocument doc = new XDocument
                (new XElement("phones", 
                    new XElement("phone",
                        new XAttribute("name", "iphone 6"),
                        new XElement("company", "apple"),
                        new XElement("price", 40000)),
                    new XElement("phone",
                        new XAttribute("name", "samsung s7"),
                        new XElement("company", "samsung"),
                        new XElement("price", 33000)),
                    new XElement("phone",
                        new XAttribute("name","nokia lumia 640"),
                        new XElement("company","nokia"),
                        new XElement("price", 35000))
                        )                        
                        );

            doc.Save(@"C:\temp\pep.xml");


            XDocument loaddoc = XDocument.Load(@"C:\temp\pep.xml");
            var items = from xe in loaddoc.Element("phones").Elements("phone")
                        where xe.Element("company").Value == "samsung"
                        orderby xe.Element("company")
                        select new Child
                        {
                            Name = xe.Attribute("name").Value,
                            Price = xe.Element("price").Value
                        };

            foreach(var item in items)
            {
                Console.WriteLine(item.Name+" "+item.Price);
            }


            var items2 = from xe in loaddoc.Element("phones").Elements("phone")
                         where xe.Element("price").Value == "35000"
                         select new Child
                         {
                             Name = xe.Attribute("name").Value,
                             Price = xe.Element("price").Value
                         };

            foreach(var item in items2)
            {
                Console.WriteLine(item.Name+" "+item.Price);
            }



            Console.ReadLine();

        }
    }
    //Сериализация представляет процесс преобразования какого-либо
    //объекта в поток байтов. После преобразования мы можем этот поток 
    //байтов или записать на диск или сохранить его временно в памяти.
    //А при необходимости можно выполнить обратный процесс - десериализацию,
    //то есть получить из потока байтов ранее сохраненный объект.

    [DataContract]
    [Serializable]
    public class Person
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Year { get; set; }

        public Person()
        {

        }

        public Person(string Name , int Year)
        {
            this.Name = Name;
            this.Year = Year;
        }
    }


    [Serializable]
    public class ComputerClass:Products
    {
        private List<Products> data = new List<Products>();
        public static int counter = 0;

        public List<Products> Data { get => data; set => data = value; }

        public void Output()
        {
            if (counter == 0)
            {
                Console.WriteLine("empty");
            }
            else
            {
                foreach (object i in Data)
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
            if (counter == 0)
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

    [Serializable]
    public class Products 
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

    
    public class Child
    {
        private string name;
        private string price;

        public string Name { get => name; set => name = value; }
        public string Price { get => price; set => price = value; }

    }
}
