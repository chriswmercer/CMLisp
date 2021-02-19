using System;

namespace CMLisp.Types
{
    public abstract class BaseType
    {
        public abstract dynamic Value { get; set; }
        public LanguageTypes Type { get; protected set; }

        private static readonly BaseType nil = new NilType();
        public static BaseType Empty => nil;

        ////public static BaseType operator +(BaseType x, BaseType y)
        ////{
        ////    try
        ////    {
        ////        switch(x.Type)
        ////        {
        ////            case LanguageTypes.String: return GeneratorFor(LanguageTypes.String, string.Concat(x.Value, y.Value));
        ////            default: return GeneratorFor(x.Type, x.Value + y.Value);
        ////        }
        ////    }
        ////    catch (Exception exc)
        ////    {
        ////        throw new ArgumentException("Failed default operator+ function. See innner exception for more details.", exc);
        ////    }
        ////}

        //public static BaseType operator -(BaseType x, BaseType y)
        //{
        //    try
        //    {
        //        return GeneratorFor(x.Type, x.Value - y.Value);
        //    }
        //    catch (Exception exc)
        //    {
        //        throw new ArgumentException("Failed default operator- function. See innner exception for more details.", exc);
        //    }
        //}

        //public static BaseType operator *(BaseType x, BaseType y)
        //{
        //    try
        //    {
        //        switch (x.Type)
        //        {
        //            case LanguageTypes.String:
        //                {
        //                    if (y.Type != LanguageTypes.Integer) throw new ArgumentException("String can be only multiplied by integer types");
        //                    string output = x.Value;
        //                    for(int i = 1; i <= y.Value; i++)
        //                    {
        //                        output += x.Value;
        //                    }
        //                    return GeneratorFor(x.Type, output);
        //                }
        //            default:  return GeneratorFor(x.Type, x.Value * y.Value);
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        throw new ArgumentException("Failed default operator* function. See innner exception for more details.", exc);
        //    }
        //}

        //public static BaseType operator /(BaseType x, BaseType y)
        //{
        //    try
        //    {
        //        return GeneratorFor(x.Type, x.Value / y.Value);
        //    }
        //    catch (Exception exc)
        //    {
        //        throw new ArgumentException("Failed default operator/ function. See innner exception for more details.", exc);
        //    }
        //}

        //public static bool operator ==(BaseType x, BaseType y)
        //{
        //    try
        //    {
        //        return x?.Value == y?.Value;
        //    }
        //    catch (Exception exc)
        //    {
        //        throw new ArgumentException("Failed default operator== function. See innner exception for more details.", exc);
        //    }
        //}

        //public static bool operator !=(BaseType x, BaseType y)
        //{
        //    try
        //    {
        //        return x.Value != y.Value;
        //    }
        //    catch (Exception exc)
        //    {
        //        throw new ArgumentException("Failed default operator!= function. See innner exception for more details.", exc);
        //    }
        //}

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
                case LanguageTypes.Integer: return new IntegerType(value);
                case LanguageTypes.String: return new StringType(value);
                default: throw new Exception("Could not determine type");
            }
        }
    }
}
