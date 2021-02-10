using System;
namespace CMLisp.Types
{
    public class DecimalType : DynamicType<Decimal>
    {
        public DecimalType(Decimal val) : base(val)
        {
            Type = LanguageTypes.Decimal;
        }
    }
}
