using System;
using System.Collections.Generic;
using System.Linq;
using CMLisp.Keywords;
using CMLisp.Language;
using CMLisp.Operations;
using CMLisp.Types;

namespace CMLisp.Language
{
    internal static class Keywords
    {
        private static Dictionary<string, Func<BaseType[], BaseType>> FunctionLookup = new Dictionary<string, Func<BaseType[], BaseType>>()
        {
            {"if", (x) => new IfKeyword().Evaluate(x) },
            {"variable", (x) => new VariableKeyword().Evaluate(x) },
            {"function", (x) => new FunctionKeyword().Evaluate(x) },
            { "print", (x) => new PrintKeyword().Evaluate(x) },
            { "printline", (x) => new PrintLineKeyword().Evaluate(x) },
            { "=>", (x) => InterpolationKeyword.Evaluate(x) },
            { "interpolate", (x) => InterpolationKeyword.Evaluate(x) }
        };

        public static Func<BaseType[], BaseType> FunctionFor(string functionName)
        {
            try
            {
                var functions = FunctionLookup.Where(fn => fn.Key.ToLower().StartsWith(functionName.ToLower()));
                if (functions.Count() != 1)
                {
                    functions = FunctionLookup.Where(fn => fn.Key.ToLower() == functionName.ToLower());
                }
                if (functions.Count() != 1) throw new MissingMethodException();

                return functions.First().Value;
            }
            catch (MissingMethodException mme)
            {
                throw new ArgumentException($"A function could not be found by using the short form { functionName }. Ensure you use enough letters - at least 3 - to differentiate the function you want.", mme);
            }
            catch
            {
                throw new ArgumentException($"Function { functionName } was not recognised.");
            }
        }

        public static bool IsKnown(string potentialKeyword)
        {
            try
            {
                var functions = FunctionLookup.Where(fn => fn.Key.ToLower().StartsWith(potentialKeyword.ToLower()));

                if (functions.Count() != 1)
                {
                    functions = FunctionLookup.Where(fn => fn.Key.ToLower() == potentialKeyword.ToLower());
                }

                if (functions.Count() != 1) throw new MissingMethodException();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
