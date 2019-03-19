using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Calculator
    {
        private Dictionary<string, Func<double, double, double>> _operations =
          new Dictionary<string, Func<double, double, double>>
          {
                {"+",(x,y) => x + y },
                {"-",(x,y) => x - y },
          };


        public double PerformOperation(string op, double x, double y)
        {
            if (!_operations.ContainsKey(op))
                throw new ArgumentException(string.Format("Operation {0} is invalid", op), "op");
            return _operations[op](x, y);
        }


        public void DefineOperation(string op, Func<double,double,double> body)
        {
            if (_operations.ContainsKey(op))
                throw new ArgumentException(string.Format("operation {0} already exists", op), "op");
            _operations.Add(op, body);
        }

    }
}
