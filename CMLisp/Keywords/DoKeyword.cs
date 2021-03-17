using System.Linq;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
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

            Evaluator.FunctionStack.Push("Do");

            foreach(var item in input)
            {
                bool returning = false;

                //check for return instruction
                if (item.Type == LanguageTypes.List)
                {
                    var listVal = item as ListContainer;
                    var innerValue = listVal.Value[0];

                    if (innerValue?.Type == LanguageTypes.Keyword && innerValue.Value.ToString().ToLower() == "return")
                    {
                        returning = true;
                    }
                }

                var evaluatedItem = Evaluator.Evaluate(item, Evaluator.LocalScope);

                while(evaluatedItem.Type == LanguageTypes.Identifier || evaluatedItem.Type == LanguageTypes.List)
                {
                    evaluatedItem = Evaluator.Evaluate(evaluatedItem, Evaluator.LocalScope);
                }

                if (returning)
                {
                    returnValue = new ArrayType(new System.Collections.Generic.List<BaseType>());
                    returnValue.Value.Add(evaluatedItem);
                    break;
                }
                else
                {
                    returnValue.Value.Add(evaluatedItem);
                }
            }

            Evaluator.FunctionStack.Pop();
            return returnValue;
        }
    }
}
