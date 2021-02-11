﻿using System;
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

            if (input.Type == LanguageTypes.List || input.Type == LanguageTypes.Vector)
            {
                returnValue += ReadList(input as ListType, indentation + 1);
            }
            else
            {
                returnValue += ReadAtom(input, indentation);
            }


            return returnValue;
        }

        private static string ReadList(ListType input, int indentation = 0)
        {
            var returnString = "";// new string('\t', indentation);

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

            returnString += $"- { input } Value= { input.Value }\n";

            return returnString;
        }
    }
}
