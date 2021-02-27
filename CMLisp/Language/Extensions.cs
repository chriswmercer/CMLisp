using System;
using System.Collections.Generic;
using CMLisp.Types;

namespace CMLisp.Language
{
    public static class Extensions
    {
        public static string ToString(List<BaseType> list)
        {
            string result = "[";
            var values = (List<BaseType>)list;

            var count = values.Count;

            for (int i = 1; i <= count; i++)
            {
                result += $"{ values[i - 1].Value.ToString()}";

                if (i != count) result += ", ";
            }
            result += "]";

            return result;
        }
    }
}
