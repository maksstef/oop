using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Classes
    {

    }
    //6.2 second part of class
    public partial class TypePartial
    {

    }
    //7 laba
    //создали 3 класса исключений , которые наследуются от стандартного класса Exception
    public class ChildException : Exception
    {
        //наследуем конструктор от Exception
        public ChildException(string msg) : base(msg) { Console.WriteLine(); }
    }

    public class ChildException1 : Exception
    {
        public ChildException1(string msg) : base(msg) { Console.WriteLine(); }
    }

    public class ChildException2 : Exception
    {
        public ChildException2(string msg) : base(msg) { Console.WriteLine(); }
    }
}
