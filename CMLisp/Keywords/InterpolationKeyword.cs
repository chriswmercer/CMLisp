using System;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public static class InterpolationKeyword
    {
        public static BaseType Evaluate(BaseType[] input)
        {
            try
            {
                if (input.Length != 2) throw new Exception();

                var source = input[0];
                var destination = input[1];

                //if we have an identifier for the source, i.e. its a variable
                //expand it
                if(source.Type == LanguageTypes.Identifier)
                {
                    source = Evaluator.Evaluate(source, Evaluator.LocalScope);
                }

                if(source.Type == LanguageTypes.Object && destination.Type == LanguageTypes.Identifier)
                {
                    Scope localScope = BuildLocalScope(source as ObjectType);
                    BaseType returnValue = Evaluator.Evaluate(destination, localScope);
                    return returnValue;
                }

                if(source.Type == LanguageTypes.Object && destination.Type == LanguageTypes.Fragment)
                {
                    foreach(var key in (source as ObjectType).Keys)
                    {
                        destination.Value = ((string)destination.Value).Replace($"%{key}%", (source as ObjectType)[key].Value.ToString());
                    }

                    return destination;
                }

                return Evaluator.Evaluate(new NilType(), Evaluator.LocalScope);
                
            }
            catch (Exception exc)
            {
                throw new LanguageException("The interpolate keyword requires a valid interpolation source and destination", exc);
            }
        }

        private static Scope BuildLocalScope(ObjectType obj)
        {
            Scope localScope = new Scope();

            foreach(var key in obj.Keys)
            {
                var id = new IdentifierType(key);
                var value = obj[key];
                var element = new ScopeElement(id, value);
                localScope.Add(element);
            }

            return localScope;
        }
    }
}
