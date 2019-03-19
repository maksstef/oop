using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Programmer
    {
        public delegate void DelegateProg(string obj);

        public event DelegateProg Rename;

        public event DelegateProg NewFeature;

        public string Name_versionn = "";

        public string Operationss = "";


        public Programmer(string Name_version, string Operations)
        {
            Name_versionn = String.Concat(Name_version);
            Name_versionn = Name_version;
            Operationss = Operations;
        }


        public void ChangeName(Programmer obj)
        {
            if(Rename != null)
            {
                Console.WriteLine("Old name : " + obj.Name_versionn);
                Console.WriteLine("Enter new name :");
                string newname = Console.ReadLine();
                Name_versionn = newname;
                Console.WriteLine($"New name : {newname}");
            }
        }


        public void AddNewFeatures(Programmer obj)
        {
            if(NewFeature != null)
            {
                Console.WriteLine("Add New Features :");
                string newFeatures = Console.ReadLine();
                Operationss = Operationss + ", " + newFeatures;
                Console.WriteLine($"features : {Operationss}");
            }
        }

    }
    
}
