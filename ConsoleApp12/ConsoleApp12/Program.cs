using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            /*DriveInfo[] drives = DriveInfo.GetDrives();
            SMSDiskInfo sms1 = new SMSDiskInfo();
            sms1.DiskInfo(drives);

            string dirName = "C:\\temp";
            SMSDirInfo sms2 = new SMSDirInfo();
            sms2.DirInfo(dirName);

            string filepath = @"C:\\temp\gg2.txt";
            SMSFileInfo sms3 = new SMSFileInfo();
            sms3.FileInfo(filepath);*/

            string textpath = @"C:\temp\smslogfile.txt";
            SMSLog sms4 = new SMSLog();

            //Console.Write("Enter your act : ");
            //string ss = Console.ReadLine();

            //sms4.LogWrite(textpath,ss);
            sms4.LogRead(textpath);

            sms4.LogFind(textpath, "pop");
            Console.WriteLine();

            Console.WriteLine("Search by word : ");
            sms4.LogFF(textpath, "pop");
            Console.WriteLine("Search by time : ");
            sms4.TimeSearch(textpath, "19:16");
            Console.WriteLine("Search be date : ");
            sms4.DateSearch(textpath, "11.2018");
            //FileManager----------------------------------//

            /*
             
            SMSFileManager smsfm = new SMSFileManager();
            //5a
            smsfm.ReadByDisk(@"C:\\");
            smsfm.CreateDir(@"C:\\temp\SMSInspect");
            smsfm.CreateTxt(@"C:\\temp\SMSInspect\smsdirinfo.txt");
            smsfm.CreateTxt(@"C:\\temp\SMSInspect\smsdirinfo2.txt");
            smsfm.SaveInfInTxt(@"C:\\temp\SMSInspect\smsdirinfo.txt");
            smsfm.CopyFile(@"C:\\temp\SMSInspect\smsdirinfo.txt", @"C:\\temp\SMSInspect\smsdirinfo2.txt");
            //temporarily
            //smsfm.RenameFile(@"C:\\temp\SMSInspect\smsdirinfo2.txt", @"C:\\temp\SMSInspect\smsdirinfoNew.txt");
            smsfm.DeleteFile(@"C:\\temp\SMSInspect\smsdirinfo.txt");


            //5b
            smsfm.CreateDir(@"C:\\temp\SMSFiles");
            //smsfm.CreateDir(@"C:\\temp\SMSFiles3");

            string[] info = { "*.txt" };
            smsfm.CopyFilesByFormat(info, @"C:\\temp\SMSInspect\", @"C:\\temp\SMSFiles\");
            //smsfm.CFBF2("*.txt", @"C:\\temp\SMSInspect\", @"C:\\temp\SMSFiles3\");

            smsfm.Move(@"C:\\temp\SMSFiLes",@"C:\\temp\SMSInspect\");

            //5c
            SMSFileManager.BoxingArchive(@"C:\\temp\SMSInspect\check.txt", @"C:\\temp\arch.gz");
            SMSFileManager.BoxingArchive2(@"C:\\temp\SMSInspect\", @"C:\\temp\arch.gz");
            smsfm.CreateTxt(@"C:\\temp\SMSInspect\unbox.txt");
            SMSFileManager.UnboxingArchive(@"C:\\temp\arch.gz", @"C:\\temp\SMSInspect\unbox.txt");
            
             */

            //6
            //sms4.LogFind(textpath,"*");

            Console.ReadLine();
        }
    }

    public class SMSLog
    {
        public void LogRead(string txtpath)
        {
            using (StreamReader sr = new StreamReader(txtpath,Encoding.Default))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
                sr.Close();
            }
        }

        public void LogWrite(string textpath, string NameOfMethod)
        {
            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(textpath, Encoding.Default))
                {
                    text = sr.ReadToEnd();
                }

                using (StreamWriter sw = new StreamWriter(textpath, false, Encoding.Default))
                {
                    sw.WriteLine(text);
                }

                using (StreamWriter sw = new StreamWriter(textpath, true, Encoding.Default))
                {
                    /*sw.WriteLine(new string('-',40));*/
                    sw.WriteLine("-----*");
                    sw.WriteLine($"Name of method : {NameOfMethod}");
                    sw.WriteLine($"Path : {textpath}");
                    sw.WriteLine($"Date/time : {DateTime.Now}");
                    sw.Close();
                 }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void LogFind(string textpath, string finderinfo)
        {
            string text = "";
            using (StreamReader sr = new StreamReader(textpath, Encoding.Default))
            {
                text = sr.ReadToEnd();
            }
            string[] arrstr = text.Split(' ');
            string[] arrstr2 = text.Split('*');

            for(int i = 0; i < arrstr.Length; ++i)
            {
                if (arrstr[i].Contains("pop"))
                {
                    Console.WriteLine("match found!");
                }
            }

        }

        public void LogFF(string textpath , string finderinfo)
        {
            string text = "";
            using (StreamReader sr = new StreamReader(textpath, Encoding.Default))
            {
                text = sr.ReadToEnd();
            }

            int count = 0;

            string[] arrss = text.Split('*');

            Regex regex = new Regex(finderinfo);
            //Regex regex = new Regex(@"pop(\w*)");// 
            // \w-соответствует любому алфавитно-цифровому символу , *-предыдущий символ повторяется 0 и более раз 

            // выражение туп(\w*) обозначает, найти все слова, 
            //которые имеют корень "туп" и после которого может 
            //стоять различное количество символов.
            //Выражение \w означает алфавитно-цифровой символ,
            //а звездочка после выражения указывает на неопределенное 
            //их количество - их может быть один, два, три или вообще не быть.

            foreach (var s in arrss)
            {
                count++;
                MatchCollection matches = regex.Matches(s);
                if(matches.Count > 0)
                {
                    foreach(Match match in matches)
                    {
                        Console.WriteLine(match.Value);
                        Console.WriteLine(s);
                    }
                }
                else
                {
                    Console.WriteLine("not found!");
                }
            }
            Console.WriteLine();
            Console.WriteLine($"Count of records :  {count}");
            Console.WriteLine();
        }


        public void TimeSearch(string path, string time)
        {
            string text = "";

            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                text = sr.ReadToEnd();
            }
            string[] arrstr = text.Split('*');

            Regex regex = new Regex(time+@"(\w*)");

            foreach(var s in arrstr)
            {
                MatchCollection matches = regex.Matches(s);
                if(matches.Count > 0)
                {
                    foreach(Match match in matches)
                    {
                        Console.WriteLine(match.Value);
                        Console.WriteLine(s);
                    }
                }
                else
                {
                    Console.WriteLine("not found!");
                }
                Console.WriteLine();
            }

        }

        public void DateSearch(string path , string date)
        {
            string text = "";
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                text = sr.ReadToEnd();
            }

            string[] arrstr = text.Split('*');

            Regex regex = new Regex(date+ @"(\w*)");

            foreach(var s in arrstr)
            {
                MatchCollection matches = regex.Matches(s);
                if(matches.Count > 0)
                {
                    foreach(Match match in matches)
                    {
                        Console.WriteLine(match.Value);
                        Console.WriteLine(s);
                    }
                }
                else
                {
                    Console.WriteLine("not found");
                }
            }

        }
    }

    public class SMSDiskInfo
    {
        public void DiskInfo(DriveInfo[] dr)
        {
            foreach (DriveInfo drive in dr)
            {
                Console.WriteLine("Name of disk : {0}",drive.Name);
                Console.WriteLine("Type : {0}",drive.DriveType);
                Console.WriteLine("Drive format : {0}",drive.DriveFormat);
                Console.WriteLine("All space : {0}",drive.TotalSize);
                Console.WriteLine("Free space : {0}",drive.TotalFreeSpace);
                Console.WriteLine("Label : {0}",drive.VolumeLabel);
                Console.WriteLine();
            }
            
        }
    }

    public class SMSFileInfo
    {
        public void FileInfo(string s)
        {
            FileInfo fi = new FileInfo(s);
            if(fi.Exists)
            {
                Console.WriteLine($"Full path : {fi.DirectoryName}");
                Console.WriteLine($"Size : {fi.Length}, expansion : {fi.Extension}, name : {fi.Name}");
                Console.WriteLine($"Time of creation : {fi.CreationTime}");
                Console.WriteLine();
            }
        }
    }

    public class SMSDirInfo
    {
        public void DirInfo(string s)
        {
            int NumOfFiles = 0;
            int NumOfSubdirs = 0;
            DirectoryInfo dirinf = new DirectoryInfo(s);
            Console.WriteLine($"Name of directory : {dirinf.Name}");

            string[] filePaths = Directory.GetFiles(s);
            foreach (var file in filePaths)
            {
                NumOfFiles++;
            }
            Console.WriteLine($"Number of files : {NumOfFiles}");

            Console.WriteLine($"Time of create : {dirinf.CreationTime}");

            string[] subdirs = Directory.GetDirectories(s);
            foreach(var subdir in subdirs)
            {
                NumOfSubdirs++;
            }
            Console.WriteLine($"Number of subdirectories : {NumOfSubdirs}");

            Console.WriteLine($"Root directory : {dirinf.Root}");
            Console.WriteLine();
        }
    }

    public class SMSFileManager 
    {
        public void ReadByDisk(string path)
        {
            if(Directory.Exists(path))
            {
                Console.WriteLine("Directories : ");
                string[] dirs = Directory.GetDirectories(path);

                foreach(string d in dirs)
                {
                    Console.WriteLine(d);
                }
                Console.WriteLine();

                Console.WriteLine("Files : ");
                string[] files = Directory.GetFiles(path);

                foreach(string f in files)
                {
                    Console.WriteLine(f);
                }
                Console.WriteLine();
            }
        }

        public void CreateDir(string path)
        {
            Directory.CreateDirectory(path);
            Console.WriteLine($"Directory created , path : {path}");
        }

        public void CreateTxt(string path)
        {
            using (File.Create(path));//using сразу закрывает поток
            Console.WriteLine($"File created , path : {path}");
            
        }

        public void SaveInfInTxt(string path)
        {

            try
            {
                Console.WriteLine("Введите строку для записи в файл:");
                string text = Console.ReadLine();

                using(StreamWriter sw = new StreamWriter(path,false, Encoding.UTF8))
                {
                    sw.Write(text);
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void CopyFile(string path, string newpath)
        {
            FileInfo fn = new FileInfo(path);
            fn.CopyTo(newpath, true);
        }

        public void RenameFile(string path, string newpath)
        {
            File.Move(path,newpath);
        }

        public void DeleteFile(string path)
        {
            FileInfo fn = new FileInfo(path);
            fn.Delete();
        }
        //Создать еще один директорий XXXFiles.
        //Скопировать в него все файлы с заданным 
        //расширением из заданного ользователем директория
        public void CopyFilesByFormat(string[] format , string path, string pathnew)
        {
            foreach (string Rembo in format)
            {
                string[] Inferno = Directory.GetFiles(path, Rembo, SearchOption.TopDirectoryOnly);
                foreach (string Mic in Inferno)
                {
                    Directory.CreateDirectory(pathnew);
                    File.Copy(Mic, pathnew + Path.GetFileName(Mic));
                }
            }
        }


        public void CFBF2(string format, string path, string npath)
        {
            DirectoryInfo dr = new DirectoryInfo(path);
            foreach(FileInfo fi in dr.GetFiles(format))
            {
                fi.CopyTo(npath + fi.Name, true);
            }
        }
        // Переместить XXXFiles в XXXInspect. 
        public void Move(string path,string npath )
        {
            DirectoryInfo dr = new DirectoryInfo(path);
            foreach(FileInfo fi in dr.GetFiles())
            {
                fi.CopyTo(npath+fi.Name, true);
                fi.Delete();
            }
            
        }
        //Сделайте архив из файлов директория XXXFiles.
        //Разархивируйте его в другой директорий
        public static void BoxingArchive(string path, string newpath)
        {
            using (FileStream ss = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (FileStream ts = File.Create(newpath))
                {
                    using (GZipStream cs = new GZipStream(ts, CompressionMode.Compress))
                    {
                        ss.CopyTo(cs);
                        Console.WriteLine("Boxing file : {0} complete ", path);
                    }
                }
            }
        }

        public static void BoxingArchive2(string path, string newpath)
        {
            DirectoryInfo dr = new DirectoryInfo(path);
            foreach(FileInfo fi in dr.GetFiles())
            {
                using (FileStream ss = new FileStream(path+fi, FileMode.OpenOrCreate))
                {
                    using (FileStream ts = File.Create(newpath))
                    {
                        using (GZipStream cs = new GZipStream(ts, CompressionMode.Compress))
                        {
                            ss.CopyTo(cs);
                            Console.WriteLine("Boxing file : {0} complete ", path);
                        }
                    }
                }
            }
            foreach(FileInfo fi in dr.GetFiles())
            {
                Console.WriteLine(path+fi);
            }
        }

        public static void UnboxingArchive(string path, string newpath)
        {
            using (FileStream ss = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (FileStream ts = File.Create(newpath))
                {
                    using (GZipStream ds = new GZipStream(ss, CompressionMode.Decompress))
                    {
                        ds.CopyTo(ts);
                        Console.WriteLine($"Unboxing file : {ts}");
                    }
                }
            }
        }

    }

}
