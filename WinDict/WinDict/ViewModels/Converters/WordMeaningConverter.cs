using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;

namespace Stachowski.WinDict.ViewModels.Converters
{
    public class WordMeaningConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var word = values.FirstOrDefault(x => x is WordViewModel);
            var firstLanguage = values.FirstOrDefault(x => x is LanguageViewModel);
            var secondLanguage = values.FirstOrDefault(x => x is LanguageViewModel && x != firstLanguage);
            var flipped = values.FirstOrDefault(x => x is bool);
            if (word == null || firstLanguage == null || secondLanguage == null || flipped == null)
                return DependencyProperty.UnsetValue;
            var language = (bool) flipped ? secondLanguage : firstLanguage;
            return
                ((WordViewModel) word).Definitions.FirstOrDefault(x => x.Key.Name == ((LanguageViewModel) language).Name).Value 
                ?? DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
