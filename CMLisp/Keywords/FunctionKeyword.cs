using System;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class FunctionKeyword
    {
        public BaseType Evaluate(BaseType[] input)
        {
            if (input.Length != 2 || input[0].Type != LanguageTypes.Identifier || input[1].Type != LanguageTypes.List)
            {
                throw new SyntaxException("The function keyword required exactly 2 parameters - an identifier and a list value");
            }

            ScopeElement element = new ScopeElement(input[0] as IdentifierType, input[1], true);

            if (Evaluator.HasLocalScope())
            {
                Evaluator.AddToLocalScope(element);
            }
            else
            {
                Evaluator.GlobalScope.Add(element);
            }

            return new NilType();
        }
    }
}
