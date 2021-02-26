using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class FunctionKeyword
    {
        public BaseType Evaluate(BaseType[] input)
        {
            if (input.Length != 3 || input[0].Type != LanguageTypes.Identifier || input[1].Value.ToString().ToLower() != "is" || input[2].Type != LanguageTypes.List)
            {
                throw new LanguageException("The function keyword requires exactly 3 parameters - an identifier, \"is\" and a list value");
            }

            if (Language.Keywords.IsKnown(input[0].Value) || ReservedWords.IsKnown(input[0].Value))
            {
                throw new LanguageException($"The identifer { input[0].Value } is a known identifier or reserved word.");
            }

            ScopeElement element = new ScopeElement(input[0] as IdentifierType, input[2], true);

            if (Evaluator.HasLocalScope())
            {
                Evaluator.AddToLocalScope(element);
            }
            else
            {
                Evaluator.GlobalScope.Add(element);
            }

            return new NilType();
        }
    }
}
