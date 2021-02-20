using System;
using CMLisp.Types;

namespace CMLisp.Operations
{
    public static class BooleanLogic
    {
        internal static BooleanType And(BaseType[] items)
        {
            return new BooleanType(true);
        }
    }
}
