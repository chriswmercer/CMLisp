using System;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class ReturnKeyword
    {
        public BaseType Evaluate(BaseType[] input)
        {
            if(input.Length != 1)
            {
                throw new LanguageException("The return keyword must only be used with 1 operand");
            }

            var evaluatedItem = input[0];

            while (evaluatedItem.Type == LanguageTypes.Identifier || evaluatedItem.Type == LanguageTypes.List)
            {
                evaluatedItem = Evaluator.Evaluate(evaluatedItem, Evaluator.LocalScope);
            }

            return evaluatedItem;
        }
    }
}
