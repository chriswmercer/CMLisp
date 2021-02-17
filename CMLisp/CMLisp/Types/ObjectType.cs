using System;
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
                            return pair.Value.Value;
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
    }
}
