using System;
using System.Globalization;
using System.Windows.Data;

namespace PersonalPlanung.Gui.Converter
{
    public class DatumZuStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var datum = (DateTime) value;
            return String.Format("{0:ddd dd. MMM yy hh:mm}", datum);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}