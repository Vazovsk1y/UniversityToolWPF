using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace UniversityTool.Infastructure.Converters
{
    class BoolToVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = (bool)value;
            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Visibility visibility = (Visibility)value;

            //return visibility switch
            //{
            //    Visibility.Visible => true,
            //    Visibility.Hidden => false,
            //    Visibility.Collapsed => false,
            //    _ => false
            //};

            throw new NotImplementedException();
        }
    }
}
