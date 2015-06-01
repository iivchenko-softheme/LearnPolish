using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using LearnPolish.Model;

namespace LearnPolish.Desktop
{
    public class LanguageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
          object parameter, CultureInfo culture)
        {
            return (Language)value == Language.Ukrainian
                ? "UA => PL"
                : "PL => UA";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return (string)value == "UA"
                ? Language.Ukrainian
                : Language.Polish;
        }
    }
}
