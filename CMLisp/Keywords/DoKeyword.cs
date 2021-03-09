using System;
using System.Linq;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class DoKeyword
    {
        public ArrayType Evaluate(BaseType[] input)
        {
            if(input.Any(item => item.Type != LanguageTypes.List))
            {
                throw new LanguageException("The do keyword only accepts 1 or more list container types");
            }

            ArrayType returnValue = new ArrayType(new System.Collections.Generic.List<BaseType>());

            foreach(var item in input)
            {
                var evaluatedItem = Evaluator.Evaluate(item, Evaluator.LocalScope);

                while(evaluatedItem.Type == LanguageTypes.Identifier || evaluatedItem.Type == LanguageTypes.List)
                {
                    evaluatedItem = Evaluator.Evaluate(evaluatedItem, Evaluator.LocalScope);
                }

                returnValue.Value.Add(evaluatedItem);
            }

            return returnValue;
        }
    }
}
