using System;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Core
{
    public static class Interpreter
    {
        public static string Interpret(string input)
        {
            try
            {
                var readResult = InputParser.ReadString(input);
                var evaluateResult = Evaluate(readResult);
                var parsedResult = OutputParser.Parse(evaluateResult);

                return parsedResult;
            }
            catch (Exception exc)
            {
                //this is where any exceptions during interpretation end up
                return exc.Message;
            }
        }

        internal static BaseType Evaluate(BaseType input, Scope scope = null, bool ignoreTestStatement = false)
        {
            Evaluator.GlobalScope = scope ?? new Scope();
            var result = Evaluator.Evaluate(input, null, ignoreTestStatement);
            return result;
        }
    }
}
