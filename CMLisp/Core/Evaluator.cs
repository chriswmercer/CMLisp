using System;
using System.Linq;
using CMLisp.Exceptions;
using CMLisp.Keywords;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Core
{
    public static class Evaluator
    {
        public static Stack FunctionStack = new Stack("init");
        public static Scope GlobalScope = new Scope();
        public static Scope LocalScope = null;

        public static BaseType Evaluate(BaseType input, Scope localScope, bool ignoreTestStatements = false)
        {
            LocalScope = localScope;
            var returnValue = EvaluateAST(input, ignoreTestStatements);
            return returnValue;
        }

        private static BaseType EvaluateAST(BaseType input, bool ignoreTestStatements)
        {
            if (input?.Type == LanguageTypes.List)
            {
                return EvaluateList(input as ListContainer, ignoreTestStatements);
            }
            else if(input?.Type == LanguageTypes.Identifier)
            {
                ScopeElement item = CheckScope(input.Value.ToString());
                if (item == null) throw new LanguageException($"The identifier { input.Value } was not found.");

                return item.Value;
            }
            else
            {
                return input ?? new NilType();
            }
        }

        private static BaseType EvaluateList(ListContainer list, bool ignoreTestStatements)
        {
            var inFunction = false;

            RemoveNils(ref list);

            if (list.Value.Count == 1 && list.Value[0].Type == LanguageTypes.List)
            {
                list = list.Value[0] as ListContainer;
                return EvaluateAST(list, ignoreTestStatements);
            }

            BaseType functor = SplitForOperation(list, out BaseType[] operands);

            if (functor.Type == LanguageTypes.Keyword)
            {
                try
                {
                    if(ignoreTestStatements && functor.Value.ToString().ToLower() == "test")
                    {
                        return new NilType();
                    }

                    var func = Language.Keywords.FunctionFor(functor.Value);
                    return func(operands);
                }
                catch (LanguageException exc)
                {
                    return CheckCatch(exc);
                }
            }
            else
            {
                if(functor.Type == LanguageTypes.Identifier)
                {
                    inFunction = true;
                    FunctionStack.Push(functor.Value);
                }

                Explode(ref list, ignoreTestStatements);
                RemoveNils(ref list);

                //need to refresh operands as they may have been evaluated out
                functor = SplitForOperation(list, out operands);

                //evaluate out operands
                if (operands != null)
                {
                    Explode(ref operands, ignoreTestStatements);
                }

                if (functor.Type == LanguageTypes.List)
                {
                    var newList = (functor as ListContainer).Value.FirstOrDefault();
                    var value = EvaluateAST(newList, ignoreTestStatements);

                    if (inFunction) FunctionStack.Pop();
                    return value;
                }

                Func<BaseType[], BaseType> func = Symbols.FunctionFor(functor.Value);

                try
                {
                    try
                    {
                        var value = func(operands);

                        if (inFunction) FunctionStack.Pop();
                        return value;
                    }
                    catch (LanguageException exc)
                    {
                        var value = CheckCatch(exc);

                        if (inFunction) FunctionStack.Pop();
                        return value;
                    }
                }
                catch (Exception exc)
                {
                    throw new EvaluationException($"It was not possible to apply the symbol { functor.Value.ToString() } to one or more operands in this expression.", exc);
                }
            }
        }

        private static void RemoveNils(ref ListContainer list)
        {
            list.Value = list.Value.Where(item => item.Type != LanguageTypes.Nil).ToList();
        }

        private static void Explode(ref ListContainer list, bool ignoreTestStatements)
        {
            for (int i = 0; i < list.Value.Count; i++)
            {
                var item = list.Value[i];

                if (item.Type == LanguageTypes.Identifier)
                {
                    list.Value[i] = EvaluateAST(item, ignoreTestStatements);
                }
                else if (item.Type == LanguageTypes.List)
                {
                    while (list.Value[i].Type == LanguageTypes.List)
                    {
                        //list.Value[i] = EvaluateAST(item);
                        list.Value[i] = EvaluateAST(list.Value[i], ignoreTestStatements);
                    }
                }
            }
        }

        private static void Explode(ref BaseType[] operands, bool ignoreTestStatements)
        {
            for (int i = 0; i < operands.Length; i++)
            {
                var item = operands[i];

                if (item.Type == LanguageTypes.Identifier)
                {
                    operands[i] = EvaluateAST(item, ignoreTestStatements);
                }
                else if (operands[i].Type == LanguageTypes.List)
                {
                    while (operands[i].Type == LanguageTypes.List)
                    {
                        operands[i] = EvaluateAST(operands[i], ignoreTestStatements);
                    }
                }
            }
        }

        internal static ScopeElement CheckScope(string input)
        {
            if (input.Contains("."))
            {
                string[] elements = input.Split('.');
                string itemName = elements[0];
                if (itemName == null || itemName.Trim() == "") return null;

                var element = CheckScope(elements[0]);
                if (element == null || (element.Value.Type != LanguageTypes.Object && element.Value.Type != LanguageTypes.Array)) return null;

                if (element.Value.Type == LanguageTypes.Array)
                {
                    if (int.TryParse(elements[1], out int index))
                    {
                        var variableValue = (element.Value as ArrayType)?.Value ?? throw new Exception($"Internal Error: Trying to get scope element { input } resulted in a non-array cast");
                        if (index >= variableValue.Count)
                        {
                            throw new LanguageException($"The index request { input } was invalid as the variable { itemName } has a count of { variableValue.Count}. Remember arrays are 0-indexed.");
                        }
                        else
                        {
                            return new ScopeElement(new IdentifierType(input), variableValue[index]);
                        }
                    }
                    else return null;
                }
                else if (element.Value.Type == LanguageTypes.Object)
                {
                    var elementAsObject = (element.Value as ObjectType) ?? throw new Exception($"Internal Error: Trying to get scope element { input } resulted in a non-object cast"); ;
                    string index = elements[1];
                    if(elementAsObject.Keys.Contains(index))
                    {
                        return new ScopeElement(new IdentifierType(input), elementAsObject[index]);
                    }
                    else
                    {
                        throw new LanguageException($"The index request { input } was invalid as the variable { itemName } has no property {index}.");
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return LocalScope?.Get(input) ?? GlobalScope.Get(input);
            }
        }

        private static BaseType SplitForOperation(ListContainer list, out BaseType[] operands)
        {
            try
            {
                BaseType returnType = null;

                var items = list.Value;
                var symbols = items.Where(item => item.Type == LanguageTypes.Symbol);
                var keywords = items.Where(item => item.Type == LanguageTypes.Keyword);
                var identifiers = items.Where(item => item.Type == LanguageTypes.Identifier);

                if (symbols.Count() > 1) throw new LanguageException("Each list must only have 1 symbol");
                if (keywords.Count() > 1) throw new LanguageException("Each list must only have 1 keyword");

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
                            throw new LanguageException("The first identifier must be a function");
                        }
                        else
                        {
                            returnType = new IdentifierType(identifier.Value);
                            operands = identifiers.Where(id => id.Value != identifier.Value).ToArray();
                        }
                    }
                    catch (Exception exc)
                    {
                        throw new LanguageException("There were no symbols, keywords, or function identifiers given. See inner exception for more details", exc);
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
            catch(Exception exc)
            {
                throw new ArgumentException("There was an error whilst trying to Split for Operation", exc);
            }
        }

        private static BaseType CheckCatch(Exception exc)
        {
            if(HasLocalScope())
            {
                ScopeElement item = LocalScope?.Get(CatchKeyword.CatchFunctionName) ?? GlobalScope.Get(CatchKeyword.CatchFunctionName);
                if(item != null && item.IsFunction)
                {
                    return item.Value;
                }
            }

            throw exc;
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
