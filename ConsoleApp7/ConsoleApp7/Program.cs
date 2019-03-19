using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp7
{
    class Program 
    {
         static void Main(string[] args)
         {
             Set<int> set1 = new Set<int>();
             Set<string> set2 = new Set<string>();
             Set<bool> set3 = new Set<bool>();
             Set<object> set4 = new Set<object>();
             Set<object> set5 = new Set<object>();
             Set<char> set6 = new Set<char>();
             Set<float> set7 = new Set<float>();
             Set<double> set8 = new Set<double>();

             set1 = set1 << 1;
             set2 = set2 << 5;
             set3 = set3 << 6;
             set4 = set4 << 3;

             Owner<object> kolo = new Owner<object>(1,"ghj","fghj");
             set1.ShowData();
             //8

             set4.Plus(set4);
             set4.Plus(kolo);
             set4.Show();
             Console.WriteLine();
             set4.Delete(set4);
             set4.Show();


            try
            {
                set4.Limit(kolo);
            }

            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            finally
            {
                Console.WriteLine("this is finally");
            }


            //Extra task
            Console.WriteLine("Enter string for record in file");
            string text = Console.ReadLine();

            //write in file
            using (FileStream fstream = new FileStream(@"C:\учёба\txt.txt",FileMode.OpenOrCreate))
            {
                byte[] arr = Encoding.Default.GetBytes(text);
                fstream.Write(arr,0,arr.Length);
                Console.WriteLine("Text recorded in file");
            }

            //read on file
            using (FileStream fstream = File.OpenRead(@"C:\учёба\txt.txt"))
            {
                byte[] arr = new byte[fstream.Length];
                fstream.Read(arr, 0, arr.Length);
                string textFromFile = Encoding.Default.GetString(arr);
                Console.WriteLine("Text from file:{0}", textFromFile);
            }

            Console.ReadKey();
        }

        

    }




}
