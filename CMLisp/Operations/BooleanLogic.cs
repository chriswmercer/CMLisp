using System.Linq;
using CMLisp.Exceptions;
using CMLisp.Types;

namespace CMLisp.Operations
{
    public static class BooleanLogic
    {
        internal static BooleanType And(BaseType[] items)
        {
            if(items.Length < 2 || items.Any(item => item.Type != LanguageTypes.Boolean))
            {
                throw new LanguageException("Boolean logic is only operable against boolean types");
            }

            return new BooleanType(items.Any(item => item.Value == false) == false);
        }
        
        internal static BooleanType Or(BaseType[] items)
        {
            if (items.Length < 2 || items.Any(item => item.Type != LanguageTypes.Boolean))
            {
                throw new LanguageException("Boolean logic is only operable against boolean types");
            }

            return new BooleanType(items.Any(item => item.Value == true));
        }

        internal static BooleanType Xor(BaseType[] items)
        {
            if (items.Length < 2 || items.Any(item => item.Type != LanguageTypes.Boolean))
            {
                throw new LanguageException("Boolean logic is only operable against boolean types");
            }

            return new BooleanType(items.Count(item => item.Value == true) == 1);
        }

        internal static BooleanType Not(BaseType[] items)
        {
            if (items.Length != 1 || items.Any(item => item.Type != LanguageTypes.Boolean))
            {
                throw new LanguageException("Boolean logic is only operable against boolean types");
            }

            return new BooleanType(!((bool)items[0].Value));
        }
    }
}
