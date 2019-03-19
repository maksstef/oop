using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.IO;


namespace ConsoleApp15
{
    class Program
    {
        static int t31 = 5;
        static int t32 = 6;
        static int t33 = 7;
        static void Main(string[] args)
        {
            /* task = new Task(() => Console.WriteLine("Hell"));
            task.Start();
            Task task2 = Task.Factory.StartNew(() => Console.WriteLine("Hell 2"));
            Task task3 = Task.Run(() => Console.WriteLine("Hell 3"));
            Console.WriteLine("end");
            */
            Console.ForegroundColor = ConsoleColor.Green;
            Stopwatch sw = new Stopwatch();
            sw.Start();


            Task taskk = new Task(NumOfErat);
            taskk.Start();
            taskk.Wait();

            var outer = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("outer starting");
                var inner = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Inner starting");
                    Thread.Sleep(2000);
                    Console.WriteLine("inner finished");
                }, TaskCreationOptions.AttachedToParent);
            }
            );
            Console.WriteLine("ID : " + outer.Id);
            Console.WriteLine(outer.Status);
            Console.WriteLine(outer.IsCompleted);
            outer.Wait();
            Console.WriteLine("end of main");

            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            Console.WriteLine("Milliseconds :" + ts.TotalMilliseconds);

            //3

            /*Rihter book*/
            /*Console.WriteLine();
            Console.WriteLine("Rihter book : ");//page 760 - ...
            CancellationTokenSource cts = new CancellationTokenSource();

            Task<Int32> t = Task.Run(() => Sum(cts.Token, 1000), cts.Token);
            Task<Int32> t2 = Task.Run(() => Sum(cts.Token, 100), cts.Token);
            Task<Int32> t3 = Task.Run(() => Sum(cts.Token, 105), cts.Token);

            Task cwt = t.ContinueWith(task => Console.WriteLine("the sum is : "+task.Result));
            cwt = t2.ContinueWith(task => Console.WriteLine("the sum is : " + task.Result));
            cwt = t3.ContinueWith(task => Console.WriteLine("the sum is : "+ task.Result));
            */


            Task<int> task_3_1 = new Task<int>(()=>FT_3(t31));
            Task<int> task_3_2 = new Task<int>(()=>FT_3(t32));
            Task<int> task_3_3 = new Task<int>(()=>FT_3(t33));
            task_3_1.Start();
            task_3_2.Start();
            task_3_3.Start();
            Task fff = new Task(()=>Console.WriteLine($"result : {task_3_1.Result+task_3_2.Result+task_3_3.Result}"));
            fff.Start();

            //4
            Task task_3_f = task_3_3.ContinueWith((Task t) => Console.WriteLine($"Sum (ContinueWith): {task_3_1.Result + task_3_2.Result + task_3_3.Result}"));

            //wait - организовывает ожидание завершения задач
            //приостанавливает исполнение вызывающего потока до тех пор,
            //пока не завершится вызываемая задача
            Task.WaitAll(task_3_1, task_3_2, task_3_3, task_3_f);
            //GetAwaiter и GetResult похожи на Result
            Console.WriteLine($"test : {task_3_3.GetAwaiter().GetResult()}");


            //5
            //parallel.for позволяет выполнять итерации цикла параллельно 
            Console.WriteLine("----------5 task---------");

            Console.WriteLine("---realization *for*---");
            Parallel.For(1, 10, FT_5);

            Console.WriteLine("---realization *foreach*---");
            ParallelLoopResult result = Parallel.ForEach<int>(new List<int>() { 1, 3, 5,6, 8 }, FT_5);
            if (!result.IsCompleted)
            {
                Console.WriteLine($"realization finished on iteration : {result.LowestBreakIteration}");
            }

            //6
            Console.WriteLine("----------6 task----------");
            Parallel.Invoke(Display2,
                () =>
                {
                    Console.WriteLine($"выполняется задача : {Task.CurrentId}");
                    Thread.Sleep(3000);
                },
                () =>
                {
                    Console.WriteLine($"complied task : {Task.CurrentId}");
                });

            //7
            Console.WriteLine("----------task 7----------");
            var bl = new BlockingCollection<Int32>(new ConcurrentQueue<Int32>());

            //поток пула получает элементы
            ThreadPool.QueueUserWorkItem(ConsumeItems, bl);

            //add to collection 5 elements
            for(Int32 item = 0;item < 5; item++)
            {
                Console.WriteLine($"producing : {item}");
                bl.Add(item);
            }

            //7 pw
            Thread.Sleep(5000);
            Console.WriteLine("-------------chtoto mutnoe------------------------");
            bc = new BlockingCollection<int>(4);

            Task Pr = new Task(producer);
            Task Cn = new Task(consumer);
            Task cn2 = new Task(consumer2);

            Pr.Start();
            Cn.Start();
            cn2.Start();

            try
            {
                Task.WaitAll(Cn, Pr,cn2);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Cn.Dispose();
                Pr.Dispose();
                cn2.Dispose();
                bc.Dispose();
            }

            //8
            Console.WriteLine("---------------8 checking----------------");
            ReadWriteAsync();
            Console.WriteLine("some work");
            
            
            Console.ReadLine();
        }

        static async void ReadWriteAsync()
        {
            string s = "fvrvrvrvr";
            using (StreamWriter writer = new StreamWriter("hello.txt", false))
            {
                await writer.WriteLineAsync(s);
            }
            using(StreamReader reader = new StreamReader("hello.txt"))
            {
                string result = await reader.ReadToEndAsync();
                Console.WriteLine(result);
            }
        }

        static BlockingCollection<int> bc;

        static void producer()
        {
            for(int i = 0; i < 5; i++)
            {
                bc.Add(i + i);
                Console.WriteLine($"поступило в продажу : {i+i}");
            }
            bc.CompleteAdding();
        }

        static void consumer()
        {
            int i;
            while (!bc.IsCompleted)
            {
                if(bc.TryTake(out i))
                {
                    Console.WriteLine($"купили : {i}");
                }
            }
        }

        static void consumer2()
        {
            int i;
            while (!bc.IsCompleted)
            {
                if(bc.TryTake(out i))
                {
                    Console.WriteLine($"другой покупатель купил : {i}");
                }
            }
        }

        /*Rihter for 7 task*/
        private static void ConsumeItems(Object o)
        {
            var bl = (BlockingCollection<Int32>)o;

            foreach(var item in bl.GetConsumingEnumerable())
            {
                Console.WriteLine($"consuming : {item}");
            }
            Console.WriteLine("All items have been consumed");
        }

        static void FT_5(int x,ParallelLoopState pls)//в *х* будут передаваться числа от 1 до 9(почему не 10?)
        {
            int result = 1;
            for(int i =1; i <= x; i++)
            {
                result *= i;
                if (i == 5)
                {
                    pls.Break();
                }
            }
            Console.WriteLine($"выполняется задача {Task.CurrentId}");
            Console.WriteLine($"result : {result}");
            Thread.Sleep(3000);
        }

        static void Display2()
        {
            Console.WriteLine($"выполняется задача : {Task.CurrentId}");
            Thread.Sleep(3000);
        }

        private static Int32 Sum(CancellationToken ct, Int32 n)
        {
            Int32 sum = 0;
            for(;n > 0; n--)
            {
                ct.ThrowIfCancellationRequested();
                checked { sum += n; }
            }
            return sum;
        }

        static int FT_3(int number)
        {
            return number + 10;
        }

        static void Display()
        {
            Console.WriteLine("start");
            Console.WriteLine("finish");
        }

        static void NumOfErat()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            int n = Int32.Parse(Console.ReadLine());

            int[] A = new int [n+1];
            for(int i = 0; i < A.Length; i++)
            {
                A[i] = i ;
            }

            Console.WriteLine("enter *y* for canceled or any symbol for continue");
            string s = Console.ReadLine();
            if (s == "y")
            {
                cts.Cancel();
            }
            else
            {
                for (int i = 2; i < n + 1; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("operation interrapted ");
                        return;
                    }
                    if (A[i] != 0)
                    {
                        Console.WriteLine(A[i]);
                        for (int j = i * i; j < n + 1; j += i)
                        {
                            A[j] = 0;
                        }
                    }
                }
            }

        }
    }
}
