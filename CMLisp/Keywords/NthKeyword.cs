using System;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class NthKeyword
    {
        public BaseType Evaluate(BaseType[] input)
        {
            if(input.Length != 2
                || (input[0].Type != LanguageTypes.Identifier && input[0].Type != LanguageTypes.Integer)
                || (input[1].Type != LanguageTypes.Identifier && input[1].Type != LanguageTypes.List && input[1].Type != LanguageTypes.Array))
            {
                throw new LanguageException("Nth must be used with 1 integer and one array type");
            }

            while (input[0].Type == LanguageTypes.Identifier)
            {
                input[0] = Evaluator.Evaluate(input[0], Evaluator.LocalScope);
            }

            while (input[1].Type == LanguageTypes.Identifier)
            {
                input[1] = Evaluator.Evaluate(input[1], Evaluator.LocalScope);
            }

            if(input[0].Type != LanguageTypes.Integer)
            {
                throw new LanguageException("Nth must be used with 1 number (either an integer or an identifier that evaluates to an integer) and one array type");
            }

            int number = (int)(input[0] as IntegerType).Value;

            while (input[1].Type == LanguageTypes.List)
            {
                input[1] = Evaluator.Evaluate(input[1], Evaluator.LocalScope);
            }

            var data = (input[1] as ArrayType)?.Value;

            if(data == null)
            {
                throw new LanguageException("Nth must be used with 1 number (either an integer or an identifier that evaluates to an integer) and one array type");
            }

            if (data.Count < number || number < 0)
            {
                throw new LanguageException($"Index of { number } was out of range.");
            }

            if (data.Count < 1)
            {
                return new NilType();
            }

            return data[number];
        }
    }
}
