using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Ed.Steamflix.Universal.Converters
{
    /// <summary>
    /// Coverts count to visibility.
    /// </summary>
    public class NotVisibleWhenZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) =>
            Equals(0d, (int)value) ? Visibility.Collapsed : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, string language) =>
            null;
    }
}
