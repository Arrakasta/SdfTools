using SdfTools.Localization;
using SdfTools.Services;
using System.Globalization;

namespace SdfTools.Examples;

/// <summary>
/// Demonstration class showing how to use the localization system
/// </summary>
public static class LocalizationExample
{
    /// <summary>
    /// Demonstrates basic localization usage
    /// </summary>
    public static void DemonstrateLocalization()
    {
        // Get current language
        var currentCulture = LocalizationService.Instance.CurrentCulture;
        Console.WriteLine($"Current culture: {currentCulture.Name}");

        // Display some localized strings
        Console.WriteLine($"Title: {LocalizedStrings.Instance.MainWindow_Title}");
        Console.WriteLine($"Language: {LocalizedStrings.Instance.MainWindow_Language}");
        Console.WriteLine($"Start Conversion: {LocalizedStrings.Instance.MainWindow_StartConversion}");

        Console.WriteLine("\nSwitching to Russian...");
        
        // Switch to Russian
        LocalizationService.Instance.SetLanguage("ru");
        
        // Display the same strings in Russian
        Console.WriteLine($"Title: {LocalizedStrings.Instance.MainWindow_Title}");
        Console.WriteLine($"Language: {LocalizedStrings.Instance.MainWindow_Language}");
        Console.WriteLine($"Start Conversion: {LocalizedStrings.Instance.MainWindow_StartConversion}");

        Console.WriteLine("\nSwitching back to English...");
        
        // Switch back to English
        LocalizationService.Instance.SetLanguage("en");
        
        // Display strings in English again
        Console.WriteLine($"Title: {LocalizedStrings.Instance.MainWindow_Title}");
        Console.WriteLine($"Language: {LocalizedStrings.Instance.MainWindow_Language}");
        Console.WriteLine($"Start Conversion: {LocalizedStrings.Instance.MainWindow_StartConversion}");
    }

    /// <summary>
    /// Demonstrates all available languages
    /// </summary>
    public static void ShowAvailableLanguages()
    {
        Console.WriteLine("Available languages:");
        
        foreach (var culture in LocalizationService.Instance.SupportedCultures)
        {
            var displayName = LocalizationService.Instance.GetCultureDisplayName(culture);
            Console.WriteLine($"- {culture.TwoLetterISOLanguageName}: {displayName} ({culture.Name})");
        }
    }

    /// <summary>
    /// Demonstrates error handling with missing keys
    /// </summary>
    public static void DemonstrateFallback()
    {
        Console.WriteLine("Testing fallback for missing key:");
        
        // This will return the key itself since it doesn't exist
        var missingString = LocalizedStrings.Instance.GetString("NonExistent_Key");
        Console.WriteLine($"Missing key result: {missingString}");
    }
}
