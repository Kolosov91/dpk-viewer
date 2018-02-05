using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace DpkViewer.Converters
{
    [ValueConversion(typeof(TimeSpan), typeof(string))]
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TimeSpan val = ((TimeSpan)value);
            return String.Format("{0}:{1}:{2}:{3}",
                val.Hours.ToString().PadLeft(2, '0'),
                val.Minutes.ToString().PadLeft(2, '0'),
                val.Seconds.ToString().PadLeft(2, '0'),
                val.Milliseconds.ToString().PadLeft(3, '0'));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
