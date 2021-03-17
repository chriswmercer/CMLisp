using System;
using System.Collections.Generic;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class TestKeyword
    {
        public BaseType Evaluate(BaseType[] input)
        {
            if (input.Length != 5 ||
                input[0].Type != LanguageTypes.Identifier ||
                input[1].Value.ToString().ToLower() != "with" ||
                input[2].Type != LanguageTypes.Object ||
                input[3].Value.ToString().ToLower() != "expect" ||
                input[4] == null)
            {
                throw new LanguageException("The test keyword requires exactly 5 parameters - an identifier, \"with\" a parameter object, \"expect\" and a basetype");
            }

            var functionName = input[0].Value;
            var parameters = input[2] as ObjectType;
            var expects = input[4] as BaseType;

            if(!Evaluator.GlobalScope.FunctionExists(functionName))
            {
                throw new LanguageException($"The test keywould could not find a function \"{ functionName }\" in the global scope");
            }

            var eval = new ListContainer(new List<BaseType>());
            eval.Value.Add(parameters);
            eval.Value.Add(new KeywordType("=>"));
            eval.Value.Add(new IdentifierType(functionName));

            var result = Interpreter.Evaluate(eval, Evaluator.GlobalScope, true);

            while (result.Type == LanguageTypes.Identifier || result.Type == LanguageTypes.List)
            {
                result = Evaluator.Evaluate(result, Evaluator.LocalScope, true);
            }

            if(result.DeepEquals(expects))
            {
                return new NilType();
            }
            else
            {
                throw new LanguageException($"Test { functionName } failed");
            }
        }
    }
}
