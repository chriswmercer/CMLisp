using System;
using System.Collections.Generic;

namespace CMLisp.Syntax
{

    public abstract class SExp
    {

    }

    public class SExpAtom : SExp
    {
        public string Value { get; private set; }

        public SExpAtom(string value)
        {
            Value = value;
        }
    }

    public class SExpList : SExp
    {
        public List<SExp> Contents { get; set; }

        public SExpList()
        {
            Contents = new List<SExp>();
        }

        public SExpList(List<SExp> tokens)
        {
            Contents = tokens;
        }
    }
}
