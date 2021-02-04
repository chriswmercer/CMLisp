using System;
namespace CMLisp.Types
{
    internal class Number : Value
    {
        public Number(object underlying) : base(underlying)
        {
        }
    }

    internal class Bool : Value
    {
        public Bool(object underlying) : base(underlying)
        {
        }
    }

    internal class Variable : Value
    {
        public Variable(object underlying) : base(underlying)
        {
        }
    }
}
