using System;
using CMLisp.Core;
using CMLisp.Types;

namespace CMLisp.Core
{
    public static class Interpreter
    {
        public static string Interpret(string input)
        {
            var readResult = Read(input);
            var evaluateResult = Evaluate(readResult);
            return evaluateResult;
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
            return Evaluator.Evaluate(input);
        }
    }
}
