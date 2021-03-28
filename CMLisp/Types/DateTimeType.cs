using System;
using System.Collections.Generic;
using System.Globalization;

namespace CMLisp.Types
{
    public class DateTimeType : DynamicType<DateTime>
    {
        public DateTimeType(DateTime val) : base(val)
        {
            this.Type = LanguageTypes.DateTime;
        }

        public static bool TryParse(object input, out DateTimeType output)
        {
            try
            {
                string inputValue = input.ToString();
                var culture = CultureInfo.GetCultureInfo("en-GB");

                var outputValue = DateTime.Parse(inputValue, culture);
                output = new DateTimeType(outputValue);
                return true;
            }
            catch
            {
                output = null;
                return false;
            }
        }

        public override string ToString()
        {
            var datetime = (DateTime)this.Value;
            return datetime.ToString("dd/MM/yyyy HH:mm:ss.ff");
        }
    }
}
