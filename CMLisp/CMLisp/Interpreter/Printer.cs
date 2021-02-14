using System;
using System.Collections.Generic;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Core
{
    public static class Printer
    {
        public static string Parse(BaseType input)
        {
            return ReadForm(input);
        }

        private static string ReadForm(BaseType input, int indentation = 0)
        {
            string returnValue = new string(' ', indentation);
            if (indentation == 0) returnValue += "ROOT\n";

            if (input.Type == LanguageTypes.List || input.Type == LanguageTypes.Array || input.Type == LanguageTypes.HashMap)
            {
                returnValue += ReadList(input as ListContainer, indentation + 1);
            }
            else
            {
                returnValue += ReadAtom(input, indentation);
            }


            return returnValue;
        }

        private static string ReadList(ListContainer input, int indentation = 0)
        {
            var returnString = "";

            returnString += $"- { input } Value= { input.Value }\n";

            foreach (var value in input.Value)
            {
                returnString += ReadForm(value, indentation);
            }

            return returnString;
        }

        private static string ReadAtom(BaseType input, int indentation = 0)
        {
            var returnString = new string(' ', indentation);

            if (input.Type == LanguageTypes.KeyValuePair)
            {
                var value = (KeyValuePair<IdentifierType, BaseType>)input.Value;

                returnString += $"- { input } Key= { value.Key.Value }, Value= { value.Value.Value }\n";
            }
            else
            {
                returnString += $"- { input } Value= { input.Value }\n";
            }

            return returnString;
        }
    }
}
