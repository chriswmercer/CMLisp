namespace CMLisp.Types
{
    public class DynamicType<T> : BaseType where T: new()
    {
        protected T internalValue;

        public DynamicType(T val)
        {
            internalValue = val;
            Type = LanguageTypes.Dynamic;
        }

        public override object Value
        {
            get => internalValue;
            set
            {
                internalValue = (T)value;
            }
        }
    }
}
