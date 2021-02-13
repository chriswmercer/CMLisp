using System;
using System.Collections.Generic;
using System.Linq;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Core
{
    public static class Evaluator
    {
        public static BaseType Evaluate(BaseType input)
        {
            return EvaluateAST(input);
        }

        private static BaseType EvaluateAST(BaseType input)
        {
            if (input.Type == LanguageTypes.List)
            {
                var list = input as ListContainer;
                var items = list.Value;
                var symbols = items.Where(item => item.Type == LanguageTypes.Symbol);
                var keywords = items.Where(item => item.Type == LanguageTypes.Keyword);

                if ((symbols.Count() + keywords.Count()) != 1) throw new SyntaxException("Each list may only have 1 keyword or 1 symbol");

                if(symbols.Any())
                {
                    BaseType[] values = items.Where(item => item.Type != LanguageTypes.Symbol).ToArray();
                    Func<BaseType[], BaseType> func = Symbols.FunctionFor(symbols.First().Value);
                    var value = func(values);
                    return value;
                }
                else
                {
                    //lookup keyword function
                    throw new NotImplementedException();

                }
            }
            else
            {
                return input;
            }
        }

        private static BaseType Apply(Func<BaseType, BaseType, BaseType> function, List<BaseType> items)
        {
            return new NilType();
        }
    }
}
