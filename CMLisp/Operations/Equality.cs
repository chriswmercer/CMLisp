using CMLisp.Types;

namespace CMLisp.Operations
{
    public static class Equality
    {
        internal static BaseType EqualityOf(BaseType[] items)
        {
            dynamic value = null;
            bool returnValue = false;

            foreach (var item in items)
            {
                if (value == null)
                {
                    value = item.Value;
                }
                else
                {
                    try
                    {
                        if (item.Value == value)
                        {
                            returnValue = true;
                        }
                        else
                        {
                            returnValue = false;
                            break;
                        }
                    }
                    catch
                    {
                        returnValue = false;
                        break;
                    }
                }
            }

            return new BooleanType(returnValue);
        }
    }
}
