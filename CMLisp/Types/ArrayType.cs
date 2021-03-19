using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CMLisp.Language;

namespace CMLisp.Types
{
    public class ArrayType : ListContainer, IEnumerable
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

        public bool Equals(ArrayType obj)
        {
            if (obj == null) return false;

            var left = this.Value.Where(x => x.Type != LanguageTypes.Nil);
            var right = obj.Value.Where(x => x.Type != LanguageTypes.Nil);
            return left.SequenceEqual(right);
        }

        public IEnumerator GetEnumerator()
        {
            return this.Value.GetEnumerator();
        }
    }
}
