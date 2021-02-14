using System;
using System.Collections.Generic;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Language
{
    public static class Symbols
    {
        private static Dictionary<string, Func<BaseType[], BaseType>> FunctionLookup = new Dictionary<string, Func<BaseType[], BaseType>>()
        {
            {"+", (x) => SumOf(x)},
            {"-", (x) => MinusSumOf(x)},
            {"*", (x) => ProductOf(x)},
            {"/", (x) => DivisorOf(x)},
            {"=", (x) => EqualityOf(x) },
            {"==", (x) => EqualityOf(x)},
            {"!=", (x) => new BooleanType(!(EqualityOf(x).Value))}
        };

        public static Func<BaseType[], BaseType> FunctionFor(string functionName)
        {
            if (!FunctionLookup.ContainsKey(functionName)) throw new ArgumentException($"Function { functionName} was not recognised.");
            return FunctionLookup[functionName];
        }

        public static bool IsKnown(string potentialSymbol)
        {
            return FunctionLookup.ContainsKey(potentialSymbol);
        }

        private static BaseType SumOf(BaseType[] items)
        {
            dynamic value = null;

            foreach(var item in items)
            {
                if (value == null)
                {
                    value = item.Value;
                }
                else
                {
                    value += item.Value;
                }
            }

            return GeneratorFor(items[0].Type, value);
        }

        private static BaseType MinusSumOf(BaseType[] items)
        {
            dynamic value = null;

            foreach (var item in items)
            {
                if (value == null)
                {
                    value = item.Value;
                }
                else
                {
                    value -= item.Value;
                }
            }

            return GeneratorFor(items[0].Type, value);
        }

        private static BaseType ProductOf(BaseType[] items)
        {
            dynamic value = null;

            foreach (var item in items)
            {
                if (value == null)
                {
                    value = item.Value;
                }
                else
                {
                    value *= item.Value;
                }
            }

            return GeneratorFor(items[0].Type, value);
        }

        private static BaseType DivisorOf(BaseType[] items)
        {
            dynamic value = null;

            foreach (var item in items)
            {
                if (value == null)
                {
                    value = item.Value;
                }
                else
                {
                    value /= item.Value;
                }
            }

            return GeneratorFor(items[0].Type, value);
        }

        private static BaseType EqualityOf(BaseType[] items)
        {
            dynamic value = null;
            bool returnValue = false;

            foreach(var item in items)
            {
                if(value == null)
                {
                    value = item.Value;
                }
                else
                {
                    try
                    {
                        if (item.Value == value)
                        {
                            returnValue = true;
                        }
                        else
                        {
                            returnValue = false;
                            break;
                        }
                    }
                    catch
                    {
                        returnValue = false;
                        break;
                    }
                }
            }

            return new BooleanType(returnValue);
        }

        private static BaseType GeneratorFor(LanguageTypes type, dynamic value)
        {
            switch(type)
            {
                case LanguageTypes.Integer: return new IntegerType(value);
                default: throw new Exception("Could not determine type");
            }
        }
    }
}
