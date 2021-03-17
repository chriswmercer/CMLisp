using System;
using System.Collections.Generic;
using System.Linq;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class TestKeyword
    {
        public static readonly string ErrorMessage = "The test keyword requires 5 or 6 parameters - an identifier, \"with\" a parameter object, \"expect\", a basetype and an optional string message to show on failure";
        public static readonly string DefaultMessage = "Tested :function with :params expected :expected but got :got";

        public BaseType Evaluate(BaseType[] input)
        {
            if (input.Length < 5 ||
                input.Length > 6 ||
                input[0].Type != LanguageTypes.Identifier ||
                input[1].Value.ToString().ToLower() != "with" ||
                (input[2].Type != LanguageTypes.Object && input[2].Type != LanguageTypes.Identifier) ||
                input[3].Value.ToString().ToLower() != "expect" ||
                input[4] == null)
            {
                throw new LanguageException(ErrorMessage);
            }

            var param = input[2];

            while(param.Type == LanguageTypes.Identifier)
            {
                param = Evaluator.Evaluate(param, Evaluator.LocalScope, true);
            }

            var functionName = input[0].Value;
            var parameters = param as ObjectType;
            var expects = input[4] as BaseType;
            var message = DefaultMessage;

            if(input.Length == 6)
            {
                if (input[5]?.Type != LanguageTypes.String) throw new LanguageException(ErrorMessage);
                message = $"{ input[5].Value.ToString() }";
            }

            if(!Evaluator.GlobalScope.FunctionExists(functionName))
            {
                throw new LanguageException($"The test keyword could not find a function \"{ functionName }\" in the global scope");
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
                if(result.Type == LanguageTypes.Array)
                {
                    result.Value = (result as ArrayType).Value.Where(x => x.Type != LanguageTypes.Nil).ToList();
                }

                if(message.Contains(":function"))
                {
                    message = message.Replace(":function", functionName);
                }

                if(message.Contains(":params"))
                {
                    message = message.Replace(":params", parameters.ToString());
                }

                if(message.Contains(":expected"))
                {
                    message = message.Replace(":expected", expects.ToString());
                }

                if(message.Contains(":got"))
                {
                    message = message.Replace(":got", result.ToString());
                }

                throw new LanguageException(message);
            }
        }
    }
}
