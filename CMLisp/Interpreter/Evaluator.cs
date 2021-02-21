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
        public static Scope GlobalScope = new Scope();
        private static Scope LocalScope = null;

        public static BaseType Evaluate(BaseType input, Scope localScope = null)
        {
            LocalScope = localScope;
            var returnValue = EvaluateAST(input);
            return returnValue;
        }

        private static BaseType EvaluateAST(BaseType input)
        {
            if (input.Type == LanguageTypes.List)
            {
                var list = input as ListContainer;

                //remove nils
                list.Value = list.Value.Where(x => x.Type != LanguageTypes.Nil).ToList();

                if (list.Value.Count == 1 && list.Value[0].Type == LanguageTypes.List)
                {
                    list = list.Value[0] as ListContainer;
                    list = EvaluateAST(list) as ListContainer;
                }

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

                        if (item.Type == LanguageTypes.Identifier)
                        {
                            list.Value[i] = EvaluateAST(item);
                        }
                        else if (item.Type == LanguageTypes.List)
                        {
                            while (list.Value[i].Type == LanguageTypes.List)
                            {
                                list.Value[i] = EvaluateAST(item);
                            }
                        }
                    }

                    //need to remove nils - these are likely functions/identifiers
                    list.Value = list.Value.Where(item => item.Type != LanguageTypes.Nil).ToList();

                    //need to refresh operands as they may have been evaluated out
                    functor = SplitForOperation(list, out operands);

                    if(functor.Type == LanguageTypes.List)
                    {
                        var newList = (functor as ListContainer).Value.FirstOrDefault();
                        return EvaluateAST(newList);
                    }

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
                ScopeElement item = LocalScope?.Get(input.Value.ToString()) ?? GlobalScope.Get(input.Value.ToString());

                if (item == null) throw new SyntaxException($"The identifier { input.Value } was not found.");

                return item.Value;
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
            var identifiers = items.Where(item => item.Type == LanguageTypes.Identifier);

            if (symbols.Count() > 1) throw new SyntaxException("Each list must only have 1 symbol");
            if (keywords.Count() > 1) throw new SyntaxException("Each list must only have 1 keyword");

            if (symbols.Any())
            {
                returnType = new SymbolType(symbols.First().Value);
                operands = items.Where(item => item.Type != LanguageTypes.Symbol && item.Type != LanguageTypes.Nil).ToArray();
            }
            else if (keywords.Any())
            {
                returnType = new KeywordType(keywords.First().Value);
                operands = items.Where(item => item.Type != LanguageTypes.Keyword && item.Type != LanguageTypes.Nil).ToArray();
            }
            else if (identifiers.Any())
            {
                var identifier = identifiers.First();

                try
                {
                    ScopeElement lookup = LocalScope?.Get(identifier.Value.ToString()) ?? GlobalScope.Get(identifier.Value.ToString());

                    if (!lookup.IsFunction)
                    {
                        throw new SyntaxException("The first identifier must be a function");
                    }
                    else
                    {
                        returnType = new IdentifierType(identifier.Value);
                        operands = identifiers.Where(id => id.Value != identifier.Value).ToArray();
                    }
                }
                catch (Exception exc)
                {
                    throw new SyntaxException("There were no symbols, keywords, or function identifiers given. See inner exception for more details", exc);
                }

            }
            else
            {
                //nil type as there is nothign to evaluate, only more lists
                returnType = new ListContainer(items);
                operands = items.ToArray();
            }

            return returnType;
        }

        private static BaseType Apply(Func<BaseType, BaseType, BaseType> function, List<BaseType> items)
        {
            return new NilType();
        }

        public static bool HasLocalScope()
        {
            return LocalScope != null;
        }

        public static void AddToLocalScope(ScopeElement element)
        {
            LocalScope.Add(element);
        }
    }
}
