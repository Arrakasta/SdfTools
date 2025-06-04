using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace SdfTools.Localization;

/// <summary>
/// Provides localized strings for the application UI.
/// Supports runtime language switching through INotifyPropertyChanged.
/// </summary>
public class LocalizedStrings : INotifyPropertyChanged
{
    private static readonly Lazy<LocalizedStrings> _instance = new(() => new LocalizedStrings());
    private readonly ResourceManager _resourceManager;
    private CultureInfo _currentCulture;

    public static LocalizedStrings Instance => _instance.Value;

    public event PropertyChangedEventHandler? PropertyChanged;

    private LocalizedStrings()
    {
        _resourceManager = new ResourceManager("SdfTools.Resources.Strings", typeof(LocalizedStrings).Assembly);
        _currentCulture = CultureInfo.CurrentUICulture;
    }

    /// <summary>
    /// Gets or sets the current culture for localization.
    /// Setting this property will update all localized strings and raise PropertyChanged events.
    /// </summary>
    public CultureInfo CurrentCulture
    {
        get => _currentCulture;
        set 
        { 
            if (_currentCulture != value) 
            { 
                _currentCulture = value;
                CultureInfo.CurrentUICulture = value;
                OnAllPropertiesChanged();
            } 
        }
    }    /// <summary>
    /// Gets a localized string by key.
    /// </summary>
    /// <param name="key">The resource key</param>
    /// <returns>The localized string or the key if not found</returns>
    public string GetString(string key)
    {
        try
        {
            return _resourceManager.GetString(key, _currentCulture) ?? key;        }
        catch
        {
            return key;
        }
    }

    #region Main Window Strings
    public string MainWindow_Title => GetString(nameof(MainWindow_Title));
    public string MainWindow_Language => GetString(nameof(MainWindow_Language));
    public string MainWindow_SelectedSchema => GetString(nameof(MainWindow_SelectedSchema));
    public string MainWindow_SelectSchemaButton => GetString(nameof(MainWindow_SelectSchemaButton));
    public string MainWindow_SourceFile => GetString(nameof(MainWindow_SourceFile));
    public string MainWindow_SelectFileButton => GetString(nameof(MainWindow_SelectFileButton));
    public string MainWindow_AttributeMapping => GetString(nameof(MainWindow_AttributeMapping));
    public string MainWindow_TargetAttribute => GetString(nameof(MainWindow_TargetAttribute));
    public string MainWindow_SourceAttribute => GetString(nameof(MainWindow_SourceAttribute));
    public string MainWindow_StartConversion => GetString(nameof(MainWindow_StartConversion));
    public string MainWindow_EditSchema => GetString(nameof(MainWindow_EditSchema));
    #endregion

    #region Schema Editor Strings
    public string SchemaEditor_Title => GetString(nameof(SchemaEditor_Title));
    public string SchemaEditor_SchemaName => GetString(nameof(SchemaEditor_SchemaName));
    public string SchemaEditor_AddAttribute => GetString(nameof(SchemaEditor_AddAttribute));
    public string SchemaEditor_AddButton => GetString(nameof(SchemaEditor_AddButton));
    public string SchemaEditor_DeleteButton => GetString(nameof(SchemaEditor_DeleteButton));
    public string SchemaEditor_ValidateSchema => GetString(nameof(SchemaEditor_ValidateSchema));
    #endregion

    #region File Dialog Filters
    public string FileDialog_SchemaFilter => GetString(nameof(FileDialog_SchemaFilter));
    public string FileDialog_GeoDataFilter => GetString(nameof(FileDialog_GeoDataFilter));
    #endregion

    #region Schema Service Messages
    public string SchemaService_AttributeAlreadyExists => GetString(nameof(SchemaService_AttributeAlreadyExists));
    public string SchemaService_DuplicateAttributes => GetString(nameof(SchemaService_DuplicateAttributes));
    public string SchemaService_SchemaValid => GetString(nameof(SchemaService_SchemaValid));
    #endregion

    #region Default Values
    public string Default_NewSchemaName => GetString(nameof(Default_NewSchemaName));
    #endregion

    private void OnAllPropertiesChanged()
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
    }
}
