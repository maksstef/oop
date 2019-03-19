using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public interface IInterfaceg<T> 
    {
        void Plus(T obj);
        void Delete(T obj);
        void Show();
    }
}
