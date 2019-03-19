using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp8
{
    class Program
    {
        delegate string Parser(string s1, string s2);

        static void Main(string[] args)
        {
            //laba first part

            Programmer Java = new Programmer("Java","No");
            Programmer CSharp = new Programmer("CSarp","No");
            Programmer Ruby = new Programmer("Ruby","Yes");

            Java.Rename += Show_Message;
            Java.NewFeature += Show_Message;
            CSharp.Rename += Show_Message;
            CSharp.NewFeature += Show_Message;
            Ruby.Rename += Show_Message;
            Ruby.NewFeature += Show_Message;

            Java.ChangeName(Java);
            Java.AddNewFeatures(Java);
            Java.Rename -= Show_Message;


            //laba second part (work with strings)
            Console.WriteLine();
            Console.WriteLine("---------- STRING PARSER ----------");
            Console.WriteLine();

            Func<string, string> p1 = s =>
             {
                 string NoZero = "";
                 for(int i = 0; i < s.Length; ++i)
                 {
                     if(s[i] != ',' && s[i] != '.' && s[i] != '!' && s[i] != '?')
                     {
                         NoZero += s[i];
                     }
                 }
                 return NoZero;
             };
            Console.WriteLine("First method : "+p1("a,b.c!d?e"));


            Func<string, string> p2 = s =>
            {
                Console.WriteLine("Add Symbols");
                string p = Console.ReadLine();
                int c = int.Parse(Console.ReadLine());
                return s = s.Insert(c, p);
            };
            Console.WriteLine("Second method : "+p2("ppp"));


            Func<string, string> p3 = s =>
             {
                 return s.ToUpper();
             };
            Console.WriteLine("Third method : "+p3("small letters"));


            Func<string, string> p4 = s =>
              {
                  string news = "";
                  for (int i = 0; i < s.Length; ++i)
                  {
                      if(s[i] == ' ' && i+1 != s.Length && s[i + 1] == ' ')
                      {

                      }
                      else
                      {
                          news += s[i];
                      }
                  }
                  return news;
              };
            Console.WriteLine("Forth method : "+p4("aa         bbbbbb bbb bbbb    "));


            Parser p5 = (s1, s2) => s1 + s2;
            Console.WriteLine("Fifth method : "+p5("aaa","bbb"));

            string ss = "olpllo";
            Console.WriteLine(ss.ToCharArray() == ss.Reverse().ToArray() ? true : false);

        }

        private static void Show_Message(string message)
        {
            Console.WriteLine(message);
        }
    }
    
}
