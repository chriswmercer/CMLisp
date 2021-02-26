using System;

namespace CMLisp.Types
{
    public abstract class BaseType
    {
        public abstract dynamic Value { get; set; }
        public LanguageTypes Type { get; protected set; }

        private static readonly BaseType nil = new NilType();
        public static BaseType Empty => nil;

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return this.Value.Equals(((BaseType)obj).Value);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        internal static BaseType GeneratorFor(LanguageTypes type, dynamic value)
        {
            switch (type)
            {
                case LanguageTypes.Array: return new ArrayType(value);
                case LanguageTypes.Boolean: return new BooleanType(value);
                case LanguageTypes.Decimal: return new DecimalType(value);
                case LanguageTypes.Fragment: return new FragmentType(value);
                case LanguageTypes.KeyValuePair: return new KeyValuePairType(value);
                case LanguageTypes.Integer: return new IntegerType(value);
                case LanguageTypes.Object: return new ObjectType(value);
                case LanguageTypes.String: return new StringType(value);
                default: throw new Exception("Could not determine type");
            }
        }

        internal static bool IsDataType(BaseType type)
        {
            switch(type.Type)
            {
                case LanguageTypes.Array:
                case LanguageTypes.Boolean:
                case LanguageTypes.Decimal:
                case LanguageTypes.Fragment:
                case LanguageTypes.Integer:
                case LanguageTypes.Object:
                case LanguageTypes.String: return true;
                default: return false;
            }
        }
    }
}
