﻿using System;
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
            if(input.Length != 3 || input[0].Type != LanguageTypes.Identifier || input[1].Value != "is")
            {
                throw new SyntaxException("The variable keyword required exactly 3 parameters - an identifier, \"is\" and any base value");
            }

            ScopeElement element = new ScopeElement(input[0] as IdentifierType, input[2]);
            Evaluator.Scope.Add(element);

            return new NilType();
        }
    }
}
