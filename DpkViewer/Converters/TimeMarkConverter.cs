using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using ConstructGraphicLibrary.Data;

namespace DpkViewer.Converters
{
    [ValueConversion(typeof(TimeMark), typeof(string))]
    public class TimeMarkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            TimeMark val = (value is TimeMark)? (TimeMark)value: new TimeMark(TimeSpan.MinValue, "Метка");
            return String.Format("{0}: [{1}:{2}:{3}:{4}]", val.Name, 
                val.Time.Hours.ToString().PadLeft(2,'0'),
                val.Time.Minutes.ToString().PadLeft(2, '0'),
                val.Time.Seconds.ToString().PadLeft(2, '0'),
                val.Time.Milliseconds.ToString().PadLeft(3, '0'));
        }
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
