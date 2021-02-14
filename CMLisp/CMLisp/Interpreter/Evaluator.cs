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
        public static Scope Scope = new Scope();

        public static BaseType Evaluate(BaseType input)
        {
            return EvaluateAST(input);
        }

        private static BaseType EvaluateAST(BaseType input)
        {
            if (input.Type == LanguageTypes.List)
            {
                var list = input as ListContainer;

                BaseType functor = SplitForOperation(list, out BaseType[] operands);

                if (functor.Type == LanguageTypes.Keyword)
                {
                    var func = Language.Keywords.FunctionFor(functor.Value);
                    return func(operands);
                }
                else
                {

                    for (int i = 0; i < list.Value.Count; i++)
                    {
                        var item = list.Value[i];

                        if (item.Type == LanguageTypes.List || item.Type == LanguageTypes.Identifier)
                        {
                            list.Value[i] = EvaluateAST(item);
                        }
                    }

                    //need to refresh operands as they may have been evaluated out
                    functor = SplitForOperation(list, out operands);
                    Func<BaseType[], BaseType> func = Symbols.FunctionFor(functor.Value);

                    try
                    {
                        var value = func(operands);
                        return value;
                    }
                    catch (Exception exc)
                    {
                        throw new EvaluationException($"It was not possible to apply the symbol { functor.Value.ToString() } to one or more operands in this expression.", exc);
                    }
                }
            }
            else if(input.Type == LanguageTypes.Identifier)
            {
                return Scope.Get(input.Value.ToString());
            }
            else
            {
                return input;
            }
        }

        private static BaseType SplitForOperation(ListContainer list, out BaseType[] operands)
        {
            BaseType returnType;

            var items = list.Value;
            var symbols = items.Where(item => item.Type == LanguageTypes.Symbol);
            var keywords = items.Where(item => item.Type == LanguageTypes.Keyword);

            if (symbols.Count() > 1) throw new SyntaxException("Each list must only have 1 symbol");
            if (keywords.Count() > 1) throw new SyntaxException("Each list must only have 1 keyword");

            if (symbols.Any())
            {
                returnType = new SymbolType(symbols.First().Value);
                operands = items.Where(item => item.Type != LanguageTypes.Symbol && item.Type != LanguageTypes.Nil).ToArray();
            }
            else
            {
                returnType = new KeywordType(keywords.First().Value);
                operands = items.Where(item => item.Type != LanguageTypes.Keyword && item.Type != LanguageTypes.Nil).ToArray();
            }

            return returnType;
        }

        private static BaseType Apply(Func<BaseType, BaseType, BaseType> function, List<BaseType> items)
        {
            return new NilType();
        }
    }
}
