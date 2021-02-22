using System;

namespace CMLisp.Types
{
    public class DecimalType : DynamicType<Decimal>
    {
        public DecimalType(decimal val) : base(val)
        {
            Type = LanguageTypes.Decimal;
        }
    }
}
