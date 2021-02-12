using System;
using System.Collections.Generic;
using CMLisp.Core;
using CMLisp.Language;

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
                var output = Interpreter.InterpretToString(input);
                Console.WriteLine(output);
            }
        }
    }
}
