﻿using System;
using CMLisp.Core;
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
            return Evaluator.Evaluate(input).Value.ToString();
        }
    }
}
