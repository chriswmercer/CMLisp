using System.Linq;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class GuardKeyword
    {
        public BaseType Evaluate(BaseType[] input)
        {
            if (input.Any(item => item.Type != LanguageTypes.Identifier)) throw new LanguageException("Error instantiating guard: one or more operands were not identifiers");

            string variablesInError = string.Empty;

            foreach(var val in input)
            {
                ScopeElement item = Evaluator.LocalScope?.Get(val.Value.ToString()) ?? Evaluator.GlobalScope.Get(val.Value.ToString());

                if (item == null) variablesInError += val.Value.ToString() + " ";
            }

            if(variablesInError != string.Empty)
            {
                throw new GuardException(variablesInError);
            }

            return new NilType();
        }
    }
}
