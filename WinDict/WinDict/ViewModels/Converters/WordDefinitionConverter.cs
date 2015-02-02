using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;

namespace Stachowski.WinDict.ViewModels.Converters
{
    public class WordDefinitionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var word = values.FirstOrDefault(x => x is WordViewModel);
            var language = values.FirstOrDefault(x => x is LanguageViewModel);
            if (word == null || language == null)
                return DependencyProperty.UnsetValue;
            return ((WordViewModel) word).Definitions[(LanguageViewModel) language];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
