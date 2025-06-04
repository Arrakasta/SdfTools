using System.Windows.Markup;

namespace SdfTools.Localization;

/// <summary>
/// XAML markup extension for localized strings.
/// Usage: {loc:Localize MainWindow_Title}
/// </summary>
[MarkupExtensionReturnType(typeof(string))]
public class LocalizeExtension : MarkupExtension
{
    public string Key { get; set; }

    public LocalizeExtension()
    {
        Key = string.Empty;
    }

    public LocalizeExtension(string key)
    {
        Key = key;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (string.IsNullOrEmpty(Key))
            return string.Empty;

        // For design-time support
        if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
        {
            return $"[{Key}]";
        }

        var property = typeof(LocalizedStrings).GetProperty(Key);
        return property?.GetValue(LocalizedStrings.Instance) ?? Key;
    }
}
