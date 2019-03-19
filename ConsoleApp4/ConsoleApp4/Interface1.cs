using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    interface IProducts
    {
        void Shop();
        void GoShop();
    }

    interface IEquipment : IProducts
    {
        void Choose();
        void NoChoose();
    }

    interface IOtherEquipment : IProducts
    {
        void Repair();
    }
}
