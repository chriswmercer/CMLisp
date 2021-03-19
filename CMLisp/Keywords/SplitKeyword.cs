using System.Collections.Generic;
using System.Linq;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class SplitKeyword
    {
        private const string Error = "The Split keyword needs 2 paramters, first of which evaluates to the string to split, second of which is the string to split on";

        public BaseType Evaluate(BaseType[] input)
        {
            if (input.Length != 2 && (input[0].Type != LanguageTypes.String || input[0].Type != LanguageTypes.Identifier) &&
                (input[1].Type != LanguageTypes.String || input[1].Type != LanguageTypes.Identifier))
            {
                throw new LanguageException(Error);
            }

            var left = Explode(input[0]);
            var right = Explode(input[1]);


            var splitValue = left.Split(right).ToList();
            var returnList = new List<BaseType>();

            foreach(var value in splitValue)
            {
                returnList.Add(new StringType(value));
            }

            return new ArrayType(returnList);
        }

        private string Explode(BaseType input)
        {
            var value = input;

            while (value.Type == LanguageTypes.Identifier || value.Type == LanguageTypes.List)
            {
                value = Evaluator.Evaluate(value, Evaluator.LocalScope);
            }

            var arr = value as StringType;

            if (arr == null) throw new LanguageException(Error);

            var innerValue = (string)arr.Value.ToString();

            return innerValue;
        }
    }
}
