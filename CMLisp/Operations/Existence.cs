using System;
using System.Collections.Generic;
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

            //if it equates to an array but that array has no values, it does not exist as a valid array
            if (item?.Value.Type == LanguageTypes.Array && (item.Value.Value as List<BaseType>).Count < 1)
            {
                return new BooleanType(false);
            }

            return new BooleanType(item != null);
        }
    }
}
