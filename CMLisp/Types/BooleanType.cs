namespace CMLisp.Types
{
    public class BooleanType : DynamicType<bool>
    {
        public BooleanType(bool val) : base(val)
        {
            Type = LanguageTypes.Boolean;
        }
    }
}
