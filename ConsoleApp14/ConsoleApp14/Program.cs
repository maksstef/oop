using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace ConsoleApp14
{
    class Program
    {
        static object locker = new object();
        static int x = 0;

        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Green;

            //1
            
            foreach (Process p in Process.GetProcesses())
            {
                Console.WriteLine($"Id : {p.Id} , name : {p.ProcessName} ");
            }
            Console.WriteLine();

            Process[] proc = Process.GetProcesses();
            foreach (var p in proc)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine();

            /*
            foreach(Process p in proc)
            {
                ProcessThreadCollection pt = p.Threads;
                foreach(ProcessThread t in pt)
                {
                    Console.WriteLine($"Thread Id : {t.Id} , start time : {t.StartTime}");
                }
            }*/

            Process procV = Process.GetProcessesByName("devenv")[0];
            ProcessThreadCollection ProcessThreads = procV.Threads;

            foreach (ProcessThread tsu in ProcessThreads)
            {
                Console.WriteLine($"thread id : {tsu.Id} , start time : {tsu.StartTime} , total proc time : {tsu.TotalProcessorTime}");
            }
            Console.WriteLine();

            //2

            //имя, детали конфигурации, все сборки, загруженные в домен. 
            //Создайте новый домен. Загрузите  туда сборку. Выгрузите домен. 

            AppDomain domain = AppDomain.CurrentDomain;
            Console.WriteLine($"Name : {domain.FriendlyName}");
            Console.WriteLine($"Setup configuration : {domain.SetupInformation}");
            Console.WriteLine($"Assemblies : {domain.GetAssemblies()}");

            AppDomain secdomain = AppDomain.CreateDomain("sec_domain");
            //событие загрузки сборки
            secdomain.AssemblyLoad += Domain_AssemblyLoad;
            //событие выгрузки домена
            secdomain.DomainUnload += SecondaryDomain_DomainUnload;

            Console.WriteLine($"Domain : {secdomain.FriendlyName}");
            secdomain.Load(new AssemblyName("sysglobl, Version=4.0.0.0, Culture=neutral, " +
                        "PublicKeyToken=b03f5f7f11d50a3a, processor architecture=MSIL"));
            Assembly[] assemblies = secdomain.GetAssemblies();
            foreach (Assembly asm in assemblies)
            {
                Console.WriteLine(asm.GetName().Name);
            }

            //выгрузка домена
            AppDomain.Unload(secdomain);

            //3

            Thread t = Thread.CurrentThread;

            Console.WriteLine($"name of thread: {t.Name}");
            t.Name = "Method Main";
            Console.WriteLine($"name of thread: {t.Name}");

            Console.WriteLine($"thread running: {t.IsAlive}");
            Console.WriteLine($"thread priority: {t.Priority}");
            Console.WriteLine($"thread status: {t.ThreadState}");

            Console.WriteLine($"Application domain: {Thread.GetDomain().FriendlyName}");
            Console.WriteLine();

            Thread myt = new Thread(new ThreadStart(Count));
            myt.Priority = ThreadPriority.Highest;
            myt.Start();

            Console.WriteLine($"name of thread: {myt.Name}");
            Console.WriteLine($"thread running: {myt.IsAlive}");
            Console.WriteLine($"thread priority: {myt.Priority}");
            Console.WriteLine($"thread status: {myt.ThreadState}");
            myt.Suspend();
            Console.WriteLine("stopped thread and start ...");
            myt.Resume();
            Thread.Sleep(5000);

            //4
            int n = 10;

            Thread th1 = new Thread(new ParameterizedThreadStart(ForThread));
            Thread th2 = new Thread(new ParameterizedThreadStart(ForThread2));
            Console.WriteLine(th1.Priority);
            Console.WriteLine(th2.Priority);

            th2.Priority = ThreadPriority.Highest;
            Console.WriteLine(th2.Priority);

            th1.Start(n);
            th2.Start(n);
            Thread.Sleep(9000);

            //4 b ( i, ii )
            Thread my1 = new Thread(Count2);
            my1.Name = "Поток 1 ";
            my1.Start();

            Thread my2 = new Thread(Count3);
            my2.Name = "Поток 2 ";
            my2.Start();
            Thread.Sleep(5000);

            //5
            int num = 4;
            TimerCallback tm = new TimerCallback(CIN);
            Timer timer = new Timer(tm, num,0, 2000);


            Console.ReadLine();
        }

        public static void CIN(object obj)
        {
            int x = (int)obj;
            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine($"timer primer : {x--}");
            }
            Console.WriteLine();
        }


        private static void SecondaryDomain_DomainUnload(object sender, EventArgs e)
        {
            Console.WriteLine("Domain load from process");
        }

        private static void Domain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("Assembly unload");
        }

        public static void Count()
        {
            string text = "";
            int n = Int32.Parse(Console.ReadLine());
            for (int i = 1; i < n; i++)
            {
                Console.WriteLine("First thread : ");
                Console.WriteLine(i * i);

                using (StreamReader sr = new StreamReader(@"C:\temp\15lab.txt", Encoding.Default))
                {
                    text = sr.ReadToEnd();
                }

                using (StreamWriter sw = new StreamWriter(@"C:\temp\15lab.txt", false, Encoding.Default))
                {
                    sw.WriteLine(text + i * i);
                }

                Thread.Sleep(400);
            }
        }

        public static void ForThread(object x)
        {
            string text = "";
            int n = (int)x;
            for (int i = 1; i < n; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);

                    using (StreamReader sr = new StreamReader(@"C:\temp\th_1_2.txt", Encoding.Default))
                    {
                        text = sr.ReadToEnd();
                    }

                    using (StreamWriter sw = new StreamWriter(@"C:\temp\th_1_2.txt", false, Encoding.Default))
                    {
                        sw.WriteLine(text + i);
                    }
                }
                Thread.Sleep(600);
            }
        }

        public static void ForThread2(object obj)
        {
            string text = "";
            int n = (int)obj;
            for (int i = 1; i < n; i++)
            {
                if (i % 2 != 0)
                {
                    Console.WriteLine(i);

                    using (StreamReader sr = new StreamReader(@"C:\temp\th_1_2.txt", Encoding.Default))
                    {
                        text = sr.ReadToEnd();
                    }

                    using (StreamWriter sw = new StreamWriter(@"C:\temp\th_1_2.txt", false, Encoding.Default))
                    {
                        sw.WriteLine(text + i);
                    }
                }
                Thread.Sleep(400);

            }
        }



        public static void Count2()
        {
            try
            {
                Monitor.Enter(locker);
                for (int i = 1; i < 9; i++)
                {
                    if(i % 2 == 0)
                    {
                        Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, i);
                        Thread.Sleep(100);
                    }

                }
            }
            finally
            {
                Monitor.Exit(locker);
            }
        }

        public static void Count3()
        {
            try
            {
                Monitor.Enter(locker);
                for (int i = 1; i < 9; i++)
                {
                    if(i % 2 != 0)
                    {
                        Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, i);
                        Thread.Sleep(100);
                    }

                }
            }
            finally
            {
                Monitor.Exit(locker);
            }
        }


    }
}

