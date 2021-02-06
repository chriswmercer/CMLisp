using System;
using System.Collections.Generic;
using CMLisp.Interpreter;
using CMLisp.Types;

namespace CMLisp.REPL
{
    public static class REPL
    {
        public static void Run()
        {
            var input = "";

            while(input.ToLower() != "exit")
            {
                Console.Write("cmlisp> ");
                input = Console.ReadLine();
                var output = Rep(input);
                Console.WriteLine(output);
            }
        }

        private static string Rep(string input)
        {
            var readResult = Read(input);
            var evaluateResult = Evaluate(readResult);
            return Print(evaluateResult);
        }

        private static BaseType Read(string input)
        {
            return Parser.ReadString(input);
        }

        private static string Evaluate(BaseType input)
        {
            return "";
        }

        private static string Print(string input)
        {
            return input;
        }
    }
}
