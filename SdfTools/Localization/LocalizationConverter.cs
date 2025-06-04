using System.Globalization;
using System.Windows.Data;

namespace SdfTools.Localization;

/// <summary>
/// Converter for binding localized strings in XAML.
/// Usage: {Binding Source={x:Static loc:LocalizedStrings.Instance}, Path=MainWindow_Title}
/// Or use with LocalizationExtension: {loc:Localize MainWindow_Title}
/// </summary>
[ValueConversion(typeof(string), typeof(string))]
public class LocalizationConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is string key && !string.IsNullOrEmpty(key))
        {
            return LocalizedStrings.Instance.GetType().GetProperty(key)?.GetValue(LocalizedStrings.Instance) ?? key;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
