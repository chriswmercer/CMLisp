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
            if(input.Length == 1)
            {
                var operand = input[0];

                while(operand?.Type == LanguageTypes.Identifier || operand?.Type == LanguageTypes.List)
                {
                    operand = Evaluator.Evaluate(operand, Evaluator.LocalScope);
                }

                if (operand.Type == LanguageTypes.Array)
                {
                    var val = (operand as ArrayType).Value;
                    val.RemoveAt(0);
                    return new ArrayType(val);
                }
                else
                {
                    return new NilType();
                }
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
