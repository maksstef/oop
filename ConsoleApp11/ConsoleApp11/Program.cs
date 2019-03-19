using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        {
            //2 question
            Type myType1 = typeof(User);
            Console.WriteLine(myType1.ToString());

            User user = new User("tom", 30);
            Type myType2 = user.GetType();
            Console.WriteLine(myType2.ToString());

            Type myType3 = Type.GetType("ConsoleApp11.User", false, true);
            Console.WriteLine(myType3.ToString());

            Type myType = Type.GetType("ConsoleApp11.User", false, true);

            Reflector reflector = new Reflector();
            reflector.B_task(myType);
            reflector.C_task(myType);
            reflector.D_task(myType);
            reflector.A_task(myType);
            Console.Write("Enter type : ");
            string name = Console.ReadLine();
            reflector.E_task(myType, name);
            reflector.F_task();


            Console.ReadLine();
        }
    }


    interface IForPrimer
    {
        void Primer();
    }


    public class User : IForPrimer
    {
        public string PrimerField;
        public string Name { get; set; }
        public int Age { get; set; }

        public User(string n, int a)
        {
            Name = n;
            Age = a;
        }

        public void Primer()
        {
            Console.WriteLine("This is primer");
        }

        public void Display()
        {
            Console.WriteLine("name: {0} , age: {1}", this.Name, this.Age);
        }

        public int Payment(int hours, int perhour)
        {
            return hours * perhour;
        }

        private static void Show()
        {
            Console.WriteLine("showing");
        }
    }

    public class Reflector
    {

        public void B_task(Type myType)
        {
            Console.WriteLine("Methods : ");
            foreach (MethodInfo method in myType.GetMethods())
            {
                string modificator = "";
                if (method.IsPublic)
                    modificator += "public ";
                if (method.IsStatic)
                    modificator += "static ";
                if (method.IsVirtual)
                    modificator += "virtual ";

                Console.Write(modificator + method.ReturnType.Name + " " + method.Name + "(");

                ParameterInfo[] parameters = method.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    Console.Write(parameters[i].ParameterType.Name + " " + parameters[i].Name);
                    if (i + 1 < parameters.Length)
                        Console.Write(", ");
                }
                Console.WriteLine(")");
            }
            Console.WriteLine();
        }


        public void E_task(Type myType, string name)
        {
            Console.WriteLine();
            MethodInfo[] methods = myType.GetMethods();
            foreach (MethodInfo m in methods)
            {
                ParameterInfo[] parameters = m.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].ParameterType.Name == name)
                    {
                        Console.Write(methods[i].Name + "(" + parameters[i].ParameterType.Name + " " + parameters[i].Name + ") ");
                        Console.WriteLine();
                    }
                }
            }

            Console.WriteLine();
        }


        public void C_task(Type myType)
        {
            Console.WriteLine("Fields : ");
            foreach (FieldInfo i in myType.GetFields())
            {
                Console.WriteLine("{0} {1}", i.FieldType, i.Name);
            }
            Console.WriteLine();

            Console.WriteLine("Properties : ");
            foreach (PropertyInfo i in myType.GetProperties())
            {
                Console.WriteLine("{0} {1}", i.PropertyType, i.Name);
            }
            Console.WriteLine();
        }


        public void D_task(Type myType)
        {
            Console.WriteLine("Interfaces : ");
            foreach (Type i in myType.GetInterfaces())
            {
                Console.WriteLine("{0}", i.Name);
            }
        }


        public void A_task(Type myType)
        {
            string AllContains = " Methods : ";
            string AllContains2 = "\n Fields : ";
            string AllContains3 = "\n Properties : ";
            string AllContains4 = "\n Interfaces : ";


            foreach (MethodInfo i in myType.GetMethods())
            {
                AllContains += i.Name + " ";
            }

            foreach (FieldInfo i in myType.GetFields())
            {
                AllContains2 += i.Name + " ";
            }

            foreach (PropertyInfo i in myType.GetProperties())
            {
                AllContains3 += i.Name + " ";
            }

            foreach (Type i in myType.GetInterfaces())
            {
                AllContains4 += i.Name + " ";
            }

            string Allstr = AllContains + AllContains2 + AllContains3 + AllContains4;
            Console.WriteLine(Allstr);


            //write in file
            using (FileStream fstream = new FileStream(@"C:\учёба\txt12.txt", FileMode.OpenOrCreate))
            {
                byte[] arr = Encoding.Default.GetBytes(Allstr);
                fstream.Write(arr, 0, arr.Length);
                Console.WriteLine("Text recorded in file");
            }

        }


        public void F_task()
        {
            string str;
            //read on file
            using (FileStream fstream = File.OpenRead(@"C:\учёба\txt12e.txt"))
            {
                byte[] arr = new byte[fstream.Length];
                fstream.Read(arr, 0, arr.Length);
                string textFromFile = Encoding.Default.GetString(arr);
                Console.WriteLine("Text from file:{0}", textFromFile);
                str = textFromFile;
            }

            string[] words = str.Split(new char[] { ' ' });

            string par1 = words[0] ;
            string par2 = words[1] ;

            void ko(string p, string p2)
            {
                Console.WriteLine(par1+" "+p2+"!");
            }

            ko(par1, par2);
        }
        
    }
}

