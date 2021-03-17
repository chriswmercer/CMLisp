using System;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class JsonKeyword
    {
        private const string Error = "Json must be used with 1 Object type";

        public BaseType Evaluate(BaseType[] input)
        {
            if (input.Length != 1 || (input[0].Type != LanguageTypes.Identifier && input[0].Type != LanguageTypes.Object))
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
