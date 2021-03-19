using System;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class StringKeyword
    {
        public BaseType Evaluate(BaseType[] input)
        {
            if (input.Length != 2 || input[0].Type != LanguageTypes.Identifier || input[1].Value.ToString().ToLower() != "as")
            {
                throw new LanguageException("The conversion keywords required exactly 2 parameters - an identifier convertable to the requested type, and \"as\". For example: (x as string)");
            }

            var value = input[0];

            while (value.Type == LanguageTypes.Identifier)
            {
                value = Evaluator.Evaluate(input[0], Evaluator.LocalScope);
            }

            if(value.Type == LanguageTypes.DateTime)
            {
                return new StringType((value as DateTimeType).ToString());
            }

            return new StringType(value.Value.ToString());
        }
    }
}
