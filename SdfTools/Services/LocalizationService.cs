using System.Globalization;
using SdfTools.Localization;

namespace SdfTools.Services;

/// <summary>
/// Service for managing application localization and language switching.
/// </summary>
public class LocalizationService
{
    private static readonly Lazy<LocalizationService> _instance = new(() => new LocalizationService());
    public static LocalizationService Instance => _instance.Value;

    private readonly Dictionary<string, CultureInfo> _supportedCultures;

    public LocalizationService()
    {
        _supportedCultures = new Dictionary<string, CultureInfo>
        {
            { "en", new CultureInfo("en-US") },
            { "ru", new CultureInfo("ru-RU") }
        };
    }

    /// <summary>
    /// Gets the list of supported cultures.
    /// </summary>
    public IEnumerable<CultureInfo> SupportedCultures => _supportedCultures.Values;

    /// <summary>
    /// Gets the current culture.
    /// </summary>
    public CultureInfo CurrentCulture => LocalizedStrings.Instance.CurrentCulture;

    /// <summary>
    /// Sets the application language.
    /// </summary>
    /// <param name="cultureCode">The culture code (e.g., "en", "ru")</param>
    public void SetLanguage(string cultureCode)
    {
        if (_supportedCultures.TryGetValue(cultureCode, out var culture))
        {
            LocalizedStrings.Instance.CurrentCulture = culture;
        }
    }

    /// <summary>
    /// Sets the application language.
    /// </summary>
    /// <param name="culture">The culture to set</param>
    public void SetLanguage(CultureInfo culture)
    {
        LocalizedStrings.Instance.CurrentCulture = culture;
    }

    /// <summary>
    /// Gets the display name for a culture in the current language.
    /// </summary>
    /// <param name="culture">The culture to get the display name for</param>
    /// <returns>The localized display name</returns>
    public string GetCultureDisplayName(CultureInfo culture)
    {
        return culture.TwoLetterISOLanguageName switch
        {
            "en" => "English",
            "ru" => "Русский",
            _ => culture.DisplayName
        };
    }
}
