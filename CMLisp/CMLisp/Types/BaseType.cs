using System;

namespace CMLisp.Types
{
    public abstract class BaseType
    {
        public abstract dynamic Value { get; set; }
        public LanguageTypes Type { get; protected set; }

        private static readonly BaseType nil = new NilType();
        public static BaseType Empty => nil;

        public static BaseType operator +(BaseType x, BaseType y)
        {
            try
            {
                return x.Value + y.Value;
            }
            catch(Exception exc)
            {
                throw new ArgumentException("Failed default operator+ function. See innner exception for more details.", exc);
            }
        }

        public static BaseType operator -(BaseType x, BaseType y)
        {
            try
            {
                return x.Value - y.Value;
            }
            catch (Exception exc)
            {
                throw new ArgumentException("Failed default operator- function. See innner exception for more details.", exc);
            }
        }

        public static BaseType operator *(BaseType x, BaseType y)
        {
            try
            {
                return x.Value * y.Value;
            }
            catch (Exception exc)
            {
                throw new ArgumentException("Failed default operator* function. See innner exception for more details.", exc);
            }
        }

        public static BaseType operator /(BaseType x, BaseType y)
        {
            try
            {
                return x.Value / y.Value;
            }
            catch (Exception exc)
            {
                throw new ArgumentException("Failed default operator/ function. See innner exception for more details.", exc);
            }
        }

        public static BaseType operator ==(BaseType x, BaseType y)
        {
            try
            {
                return x.Value == y.Value;
            }
            catch (Exception exc)
            {
                throw new ArgumentException("Failed default operator== function. See innner exception for more details.", exc);
            }
        }

        public static BaseType operator !=(BaseType x, BaseType y)
        {
            try
            {
                return x.Value != y.Value;
            }
            catch (Exception exc)
            {
                throw new ArgumentException("Failed default operator!= function. See innner exception for more details.", exc);
            }
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
    }
}
