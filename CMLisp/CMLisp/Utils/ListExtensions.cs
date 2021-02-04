using System;
using System.Collections.Generic;
using CMLisp.Syntax;

namespace CMLisp.Utils
{
    public static class ListExtensions
    {
        public static string Pop(this List<string> list)
        {
            list.RemoveAt(0);
            return list[0];
        }
    }
}
