using System;
namespace CMLisp.Types
{
    public class IntegerType : DynamicType<int>
    {
        public IntegerType(int val) : base(val)
        {
            Type = LanguageTypes.Integer;
        }
    }
}
