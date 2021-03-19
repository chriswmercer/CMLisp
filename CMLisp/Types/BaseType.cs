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

            return DeepEquals(obj as BaseType);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool DeepEquals(BaseType obj)
        {
            if (obj == null) return false;
            if (this.Type != obj.Type) return false;

            switch(this.Type)
            {
                case LanguageTypes.Array: return (this as ArrayType).Equals(obj as ArrayType);
                case LanguageTypes.KeyValuePair: return (this as KeyValuePairType).Equals(obj as KeyValuePairType);
                case LanguageTypes.Object: return (this as ObjectType).Equals(obj as ObjectType);
                default: return this.Value == obj.Value;
            }
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
                case LanguageTypes.DateTime: return new DateTimeType(value);
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
                case LanguageTypes.String:
                case LanguageTypes.DateTime: return true;
                default: return false;
            }
        }
    }
}
