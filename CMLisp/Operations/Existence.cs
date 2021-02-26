using System;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Operations
{
    public static class Existence
    {
        internal static BaseType Exists(BaseType[] items)
        {
            if (items.Length != 1 || items[0].Type != LanguageTypes.Identifier)
            {
                throw new LanguageException("The exists operator only evaluates 1 operand which must be an identifier");
            }

            ScopeElement item = Evaluator.CheckScope(items[0].Value.ToString());

            return new BooleanType(item != null);
        }
    }
}
