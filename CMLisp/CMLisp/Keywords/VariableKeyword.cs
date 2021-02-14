using System;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class VariableKeyword
    {
        public BaseType Evaluate(BaseType[] input)
        {
            if(input.Length != 2 || input[0].Type != LanguageTypes.Identifier)
            {
                throw new SyntaxException("The variable keyword required exactly 2 parameters - an identifier and any base value");
            }

            ScopeElement element = new ScopeElement(input[0] as IdentifierType, input[1]);
            Evaluator.Scope.Add(element);

            return new NilType();
        }
    }
}
