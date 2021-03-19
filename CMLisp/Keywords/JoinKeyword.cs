using System.Linq;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class JoinKeyword
    {
        private const string Error = "The Join keyword needs 1 paramter, which evaluates to an array";

        public BaseType Evaluate(BaseType[] input)
        {
            if(input.Length != 1 && (input[0].Type != LanguageTypes.Array || input[0].Type != LanguageTypes.Identifier))
            {
                throw new LanguageException(Error);
            }

            var value = input[0];

            while(value.Type == LanguageTypes.Identifier)
            {
                value = Evaluator.Evaluate(value, Evaluator.LocalScope);
            }

            var arr = value as ArrayType;

            if (arr == null) throw new LanguageException(Error);

            var innerValue = arr.Value;
            var stringValue = innerValue.Select(x => (string)((x.Value).ToString())).ToArray();

            var returnValue = string.Join("", stringValue);

            return new StringType(returnValue);
        }
    }
}
