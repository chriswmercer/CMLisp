using System;
using CMLisp.Exceptions;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class ReverseKeyword
    {
        public BaseType Evaluate(BaseType[] input)
        {
            if(input.Length != 1)
            {
                throw new LanguageException("The Reverse keyword must be passed one, and only one operand.");
            }

            var value = input[0];

            switch(value.Type)
            {
                case LanguageTypes.Array: return Reverse(value as ArrayType);
                case LanguageTypes.Boolean: return Reverse(value as BooleanType);
                case LanguageTypes.Decimal: return Reverse(value as DecimalType);
                case LanguageTypes.Fragment: return Reverse(value as FragmentType);
                case LanguageTypes.Integer: return Reverse(value as IntegerType);
                case LanguageTypes.String: return new StringType(Reverse(value.Value.ToString()));
                default: throw new LanguageException("For reverse keyword, supported types are Array, Boolean, Decimal, Fragment, Integer and String");
            }
        }

        private BaseType Reverse(ArrayType value)
        {
            value.Value.Reverse();
            return value;
        }

        private BaseType Reverse(BooleanType value)
        {
            var innerValue = (bool)value.Value;
            return new BooleanType(!innerValue);
        }

        private BaseType Reverse(DecimalType value)
        {
            var innerValue = (decimal)value.Value;
            string innerValueString = innerValue.ToString();
            innerValueString = Reverse(innerValueString);
            var innerValueDecimal = Convert.ToDecimal(innerValueString);
            return new DecimalType(innerValueDecimal);
        }

        private BaseType Reverse(FragmentType value)
        {
            return new FragmentType(Reverse(value.Value.ToString()));
        }

        private BaseType Reverse(IntegerType value)
        {
            var innerValue = (int)value.Value;
            string innerValueString = innerValue.ToString();
            innerValueString = Reverse(innerValueString);
            var innerValueInt = int.Parse(innerValueString);
            return new IntegerType(innerValueInt);
        }



        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
