using System.Collections.Generic;

namespace CMLisp.Types
{
    public class KeyValuePairType : DynamicType<KeyValuePair<IdentifierType, BaseType>>
    {
        public KeyValuePairType(KeyValuePair<IdentifierType, BaseType> val) : base(val)
        {
            Type = LanguageTypes.KeyValuePair;
        }

        public bool Equals(KeyValuePairType obj)
        {
            if (obj == null) return false;

            var left = (KeyValuePair<IdentifierType, BaseType>)this.Value;
            var right = (KeyValuePair<IdentifierType, BaseType>)obj.Value;

            return (left.Key.Equals(right.Key) && left.Value.Equals(left.Value));
        }
    }
}
