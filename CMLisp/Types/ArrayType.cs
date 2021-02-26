using System.Collections.Generic;
using CMLisp.Language;

namespace CMLisp.Types
{
    public class ArrayType : ListContainer
    {
        public ArrayType(List<BaseType> val) : base(val)
        {
            Type = LanguageTypes.Array;
        }

        public override string ToString()
        {
            string result = "[";
            var values = (List<BaseType>)this.Value;

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
