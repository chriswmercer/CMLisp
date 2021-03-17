using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CMLisp.Language;

namespace CMLisp.Types
{
    public class ObjectType : ListContainer
    {
        public ObjectType(List<BaseType> val) : base(val)
        {
            Type = LanguageTypes.Object;
        }

        public BaseType this[string key]
        {
            get
            {
                foreach(var value in this.Value)
                {
                    var kvp = value as KeyValuePairType;

                    if (kvp is KeyValuePairType)
                    {
                        var pair = (KeyValuePair<IdentifierType, BaseType>)kvp.Value;
                        if (pair.Key.Value == key)
                        {
                            var innerValue = pair.Value;
                            return innerValue;
                        }
                    }
                }

                throw new KeyNotFoundException($"Key { key } was not found.");
            }
        }

        public BaseType this[BaseType key]
        {
            get
            {
                foreach (var value in this.Value)
                {
                    var kvp = value as KeyValuePairType;

                    if (kvp is KeyValuePairType)
                    {
                        var pair = (KeyValuePair<IdentifierType, BaseType>)kvp.Value;
                        if (pair.Key.Value == key.Value)
                        {
                            return pair.Value;
                        }
                    }
                }

                throw new KeyNotFoundException($"Key { key } was not found.");
            }
        }

        public string[] Keys
        {
            get
            {
                List<string> keys = new List<string>();

                foreach (var value in this.Value)
                {
                    var kvp = value as KeyValuePairType;
                    var key = (KeyValuePair<IdentifierType, BaseType>)kvp.Value;
                    keys.Add(key.Key.Value);
                }

                return keys.ToArray();
            }
        }

        public bool Equals(ObjectType obj)
        {
            if (obj == null) return false;

            var left = this.Value;
            var right = obj.Value;

            if (left.Count != right.Count) return false;

            for(int i = 0; i < left.Count; i++)
            {
                var valueLeft = left[i] as KeyValuePairType;
                var valueRight = right[i] as KeyValuePairType;
                if (valueLeft == null || valueRight == null) return false;
                if (!valueLeft.Equals(valueRight)) return false;
            }

            return true;
        }
    }
}
