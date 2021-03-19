using System;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class JsonKeyword
    {
        private const string Error = "The conversion keywords required exactly 2 parameters - an identifier convertable to the requested type, and \"as\". For example: (x as json)";

        public BaseType Evaluate(BaseType[] input)
        {
            if (input.Length != 2 || (input[0].Type != LanguageTypes.Identifier && input[0].Type != LanguageTypes.Object) || input[1].Value.ToString().ToLower() != "as")
            {
                throw new LanguageException(Error);
            }

            var value = input[0];

            while (value.Type == LanguageTypes.Identifier)
            {
                value = Evaluator.Evaluate(input[0], Evaluator.LocalScope);
            }

            var obj = value as ObjectType;

            if(obj == null)
            {
                throw new LanguageException(Error);
            }

            return new StringType(obj.ToJson());
        }
     }
}
