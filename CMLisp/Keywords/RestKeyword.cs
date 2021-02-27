using System;
using System.Linq;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class RestKeyword
    {
        public BaseType Evaluate(BaseType[] input)
        {
            if (input.Length == 1 && input[0].Type == LanguageTypes.Array)   
            {
                var val = (input[0] as ArrayType).Value;
                val.RemoveAt(0);
                return new ArrayType(val);
            }
            else if(input.Length == 1)
            {
                return new NilType();
            }
            else
            {
                var inlist = input.ToList();
                inlist.RemoveAt(0);
                return new ListContainer(inlist);
            }
        }
    }
}
