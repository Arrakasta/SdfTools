using System.Globalization;
using System.Windows.Data;
using SdfTools.Services;

namespace SdfTools.Converters;

/// <summary>
/// Converter for displaying culture names in a localized format.
/// </summary>
[ValueConversion(typeof(CultureInfo), typeof(string))]
public class CultureDisplayNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CultureInfo cultureInfo)
        {
            return LocalizationService.Instance.GetCultureDisplayName(cultureInfo);
        }
        return value?.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
