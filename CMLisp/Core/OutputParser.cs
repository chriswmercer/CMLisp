using System.Collections.Generic;
using CMLisp.Types;

namespace CMLisp.Core
{
    public static class OutputParser
    {
        public static string Parse(BaseType input)
        {
            var returnString = string.Empty;

            switch (input.Type)
            {
                case LanguageTypes.Array: returnString += ParseArray(input as ArrayType); break;
                case LanguageTypes.Nil: break;
                default: returnString += input.Value.ToString(); break;
            }

            return returnString;
        }

        private static string ParseArray(ArrayType input)
        {
            var returnString = string.Empty;
            List<string> entries = new List<string>();

            foreach (var item in input.Value)
            {
                var result = Parse(item);
                if (result != string.Empty) entries.Add(result);
            }

            if (entries.Count == 0)
            {
                return "Nil";
            }
            else if (entries.Count == 1)
            {
                returnString = entries[0];
            }
            else
            {
                returnString = "[" + string.Join(',', entries.ToArray()) + "]";
            }

            return returnString;
        }
    }
}
