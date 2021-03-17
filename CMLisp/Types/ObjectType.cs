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

        public override string ToString()
        {
            return ReadForm(this, false);
        }

        public string ToJson()
        {
            return ReadForm(this, true);
        }

        private static string ReadForm(BaseType input, bool jsonMode, int indentation = 0)
        {
            var spacer = jsonMode ? '\t' : ' ';
            string returnValue = new string(spacer, indentation);

            if (input.Type == LanguageTypes.Object)
            {
                returnValue += ReadObject(input as ObjectType, jsonMode, indentation);
            }
            else
            {
                returnValue += ReadAtom(input, jsonMode, indentation + 1);
            }

            return returnValue;
        }

        private static string ReadObject(ObjectType input, bool jsonMode, int indentation = 0)
        {
            if (input.Value.Count == 0) return "{}";

            var returnString = "{ ";

            if (jsonMode) returnString += "\n";

            for(int i = 0; i < input.Value.Count; i++)
            {
                var value = input.Value[i];
                returnString += ReadForm(value, jsonMode, indentation);

                if (i != (input.Value.Count - 1))
                {
                    returnString += ", ";
                }
                else
                {
                    returnString += " ";
                }

                if (jsonMode) returnString += "\n";
            }

            returnString += "}";
            if (jsonMode) returnString += "\n";

            return returnString;
        }

        private static string ReadAtom(BaseType input, bool jsonMode, int indentation = 0)
        {
            var spacer = jsonMode ? '\t' : ' ';
            var returnString = new string(spacer, indentation);
            var kvp = (KeyValuePair<IdentifierType, BaseType>)input.Value;
            var key = kvp.Key.Value.ToString();
            var valueBase = BaseType.GeneratorFor(kvp.Value.Type, kvp.Value.Value);
            var value = valueBase.ToString();

            returnString += $"{ key } : { value }";
            return returnString;
        }
    }
}
