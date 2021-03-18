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
            if (Language.Keywords.IsKnown(input[0].Value) || ReservedWords.IsKnown(input[0].Value))
            {
                throw new LanguageException($"The identifer { input[0].Value } is a known keyword or reserved word.");
            }

            if (input.Length == 3)
            {
                if (input[0].Type != LanguageTypes.Identifier || input[1].Value.ToString().ToLower() != "is" || input[2].Type != LanguageTypes.List)
                {
                    throw new LanguageException("The function keyword needs the following paramters: an identifier, \"is\" an optional test statement and a list value");
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
            else if (input.Length == 4)
            {
                if (input[0].Type != LanguageTypes.Identifier ||
                    input[1].Value.ToString().ToLower() != "is" ||
                    !IsTestStatment(input[2]) ||
                    input[3].Type != LanguageTypes.List)
                {
                    throw new LanguageException("The function keyword needs the following paramters: an identifier, \"is\" an optional test statement and a list value");
                }

                ScopeElement tempElement = new ScopeElement(input[0] as IdentifierType, input[3], true);

                if (Evaluator.HasLocalScope())
                {
                    Evaluator.AddToLocalScope(tempElement);
                }
                else
                {
                    Evaluator.GlobalScope.Add(tempElement);
                }

                var result = Evaluator.Evaluate(input[2], Evaluator.LocalScope);

                while(result.Type == LanguageTypes.Identifier || result.Type == LanguageTypes.List)
                {
                    result = Evaluator.Evaluate(result, Evaluator.LocalScope, true);
                }

                return new NilType();
            }
            else
            {
                throw new LanguageException("The function keyword requires exactly 3 parameters - an identifier, \"is\" and a list value");
            }
        }

        private bool IsTestStatment(BaseType statement)
        {
            if (statement.Type != LanguageTypes.List) return false;

            var testStatement = statement as ListContainer;
            if (testStatement == null) return false;

            var innerValue = testStatement.Value[0];
            if (innerValue.Type != LanguageTypes.Keyword && innerValue.Value.ToString().ToLower() != "test") return false;

            return true;
        }
    }
}
