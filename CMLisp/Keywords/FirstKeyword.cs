using System;
using System.Linq;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class FirstKeyword
    {
        public BaseType Evaluate(BaseType[] input)
        {
            IntegerType first = new IntegerType(0);
            var list = input.ToList();
            list.Insert(0, first);
            return new NthKeyword().Evaluate(list.ToArray());
        }
    }
}
