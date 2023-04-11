using System;
using System.Globalization;
using System.Windows.Data;

namespace UniversityTool.Infastructure.Converters
{
    internal class BoolToColumnWidthConverter : IValueConverter
    {
        private const string COLUMN_WIDTH_IF_TRUE = "0";
        private const string COLUMN_WIDTH_IF_FALSE = "2*";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? COLUMN_WIDTH_IF_TRUE : COLUMN_WIDTH_IF_FALSE;
            }
            return COLUMN_WIDTH_IF_FALSE;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
