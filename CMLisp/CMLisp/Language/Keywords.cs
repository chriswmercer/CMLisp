using System;
using System.Collections.Generic;
using System.Linq;
using CMLisp.Keywords;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Language
{
    internal static class Keywords
    {
        private static Dictionary<string, Func<BaseType[], BaseType>> FunctionLookup = new Dictionary<string, Func<BaseType[], BaseType>>()
        {
            {"if", (x) => new IfKeyword().Evaluate(x) },
            {"variable", (x) => new VariableKeyword().Evaluate(x) },
            {"function", (x) => new FunctionKeyword().Evaluate(x) }
        };

        public static Func<BaseType[], BaseType> FunctionFor(string functionName)
        {
            if (!FunctionLookup.ContainsKey(functionName)) throw new ArgumentException($"Function { functionName } was not recognised.");
            return FunctionLookup[functionName];
        }

        public static bool IsKnown(string potentialKeyword)
        {
            return FunctionLookup.ContainsKey(potentialKeyword.ToLower());
        }
    }
}
