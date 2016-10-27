using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    /// <summary>
    /// Converts a comma seperated string to a list of enums of the specified type
    /// </summary>
    /// <typeparam name="T">Type to convert strings to</typeparam>
    public class CommaSeperatedStringToListOfEnumConverter<T> : TypeConverter where T : struct, IConvertible
    {
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value != null && typeof(T).IsEnum)
            {
                var vals = ((string) value).Split(',').Select(x => (T)Enum.Parse(typeof(T), x));
                return new EnumTypeList<T>() { CompressionTypes = vals };
            }

            return new EnumTypeList<T>();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return typeof(string) == sourceType;
        }

    }
}
