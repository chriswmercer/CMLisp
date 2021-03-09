using System;
using System.Collections.Generic;
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
                var readResult = Read(input);
                var evaluateResult = Evaluate(readResult);

                //we don't want to display nils
                if (evaluateResult == "Nil") evaluateResult = string.Empty;

                return evaluateResult;
            }
            catch (Exception exc)
            {
                //this is where any exceptions during interpretation end up
                return exc.Message;
            }
        }

        public static string InterpretToString(string input)
        {
            var readResult = Read(input);
            var printResult = Printer.Parse(readResult);
            return printResult;
        }

        private static BaseType Read(string input)
        {
            return Parser.ReadString(input);
        }

        private static string Evaluate(BaseType input)
        {
            Evaluator.GlobalScope = new Scope();
            var result = Evaluator.Evaluate(input, null);
            return EvaluationParser(result);
        }

        private static string EvaluationParser(BaseType input)
        {
            var returnString = string.Empty;

            switch (input.Type)
            {
                case LanguageTypes.Array: returnString += EvaluationArray(input as ArrayType); break;
                case LanguageTypes.Nil: break;
                default: returnString += input.Value.ToString(); break;
            }

            return returnString;
        }

        private static string EvaluationArray(ArrayType input)
        {
            var returnString = "[";

            foreach(var item in input.Value)
            {
                returnString += EvaluationParser(item) + ",";
            }

            returnString = returnString.Substring(0, returnString.Length - 1) + "]";

            return returnString;
        }
    }
}
