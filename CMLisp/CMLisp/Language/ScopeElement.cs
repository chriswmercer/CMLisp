using System;
using CMLisp.Types;

namespace CMLisp.Language
{
    public class ScopeElement
    {
        public IdentifierType Identifier;
        public string Label;
        public bool IsFunction;
        public BaseType Value;

        public ScopeElement(IdentifierType id, BaseType value, bool isFunction = false)
        {
            Identifier = id;
            Label = id.Value.ToString();
            IsFunction = isFunction;
            Value = value;
        }
    }
}
