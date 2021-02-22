using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class CatchKeyword
    {
        public static string CatchFunctionName = "catch_fun";

        public BaseType Evaluate(BaseType[] input)
        {
            if (input.Length != 1 || input[0].Type != LanguageTypes.List)
            {
                throw new LanguageException("The catch keyword required exactly 1 parameter - a list container value");
            }

            ScopeElement element = new ScopeElement(new IdentifierType(CatchFunctionName), input[0], true);

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
