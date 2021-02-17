using System;
using System.Collections.Generic;

namespace CMLisp.Types
{
    public class KeyValuePairType : DynamicType<KeyValuePair<IdentifierType, BaseType>>
    {
        public KeyValuePairType(KeyValuePair<IdentifierType, BaseType> val) : base(val)
        {
            Type = LanguageTypes.KeyValuePair;
        }
    }
}
