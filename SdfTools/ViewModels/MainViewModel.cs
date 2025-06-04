using Microsoft.Win32;
using SdfTools.Abstracts;
using SdfTools.Localization;
using SdfTools.Services;
using SdfTools.Utilities;
using SdfTools.Views;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace SdfTools.ViewModels;

public class MainViewModel : ViewModelBase
{
    // Paths to selected files
    private string? _selectedSchemaPath;
    public string? SelectedSchemaPath
    {
        get => _selectedSchemaPath;
        set
        {
            if (Set(ref _selectedSchemaPath, value))
            {
                TryAutoMap();
            }
        }
    }

    private string? _selectedFilePath;
    public string? SelectedFilePath
    {
        get => _selectedFilePath;
        set
        {
            if (Set(ref _selectedFilePath, value))
            {
                TryAutoMap();
            }
        }
    }

    // Collection of source attributes obtained when importing a file
    public ObservableCollection<string> SourceAttributes { get; set; } = new();

    // Collection of mappings (items where TargetAttributeName is an attribute from the schema, and SelectedSourceAttribute is the selected source)
    public ObservableCollection<MappingItem> MappingItems { get; set; } = [];

    // Commands
    public ICommand SelectSchemaCommand { get; }
    public ICommand SelectFileCommand { get; }
    public ICommand StartConversionCommand { get; }
    public ICommand EditSchemaCommand { get; }

    // Language switching
    private CultureInfo _selectedLanguage;
    public CultureInfo SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            if (Set(ref _selectedLanguage, value))
            {
                LocalizationService.Instance.SetLanguage(value);
            }
        }
    }

    public ObservableCollection<CultureInfo> AvailableLanguages { get; }

    public MainViewModel()
    {
        // Initialize language settings
        AvailableLanguages = new ObservableCollection<CultureInfo>(LocalizationService.Instance.SupportedCultures);
        _selectedLanguage = LocalizationService.Instance.CurrentCulture;

        SelectSchemaCommand = new RelayCommand(SelectSchema);
        SelectFileCommand = new RelayCommand(SelectFile);
        StartConversionCommand = new RelayCommand(StartConversion, CanStartConversion);
        EditSchemaCommand = new RelayCommand(EditSchema, CanEditSchema);
    }    // Command to select schema
    private void SelectSchema(object? parameter)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = LocalizedStrings.Instance.FileDialog_SchemaFilter
        };
        if (openFileDialog.ShowDialog() != true) return;

        var filePath = openFileDialog.FileName;

        if (!string.IsNullOrEmpty(filePath))
        {
            SelectedSchemaPath = filePath;
            // Here you can add logic to load the schema and populate MappingItems.TargetAttributeName
            LoadSchema(SelectedSchemaPath);
        }
    }    // Command to select source file
    private void SelectFile(object? parameter)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = LocalizedStrings.Instance.FileDialog_GeoDataFilter
        };
        if (openFileDialog.ShowDialog() != true) return;

        var filePath = openFileDialog.FileName;

        if (!string.IsNullOrEmpty(filePath))
        {
            SelectedFilePath = filePath;
            // Here you can add logic to import data using FDO to populate SourceAttributes
            ImportSourceAttributes(SelectedFilePath);
        }
    }

    // Simulate loading schema (populate target attribute list)
    private void LoadSchema(string schemaPath)
    {
        // Example: load target schema from file and populate MappingItems
        // In a real implementation, the schema will be deserialized into a list of objects
        var targetAttributes = new[] { "FeatId", "Contractor", "Address", "CreatedDate", "LastUpdate" };

        MappingItems.Clear();
        foreach (var attr in targetAttributes)
        {
            MappingItems.Add(new MappingItem { TargetAttributeName = attr });
        }

        // After loading the schema, you can try auto-mapping
        TryAutoMap();
    }

    // Simulate importing source attributes from file
    private void ImportSourceAttributes(string filePath)
    {
        // Example: get a list of attributes from the source file
        // In a real implementation, you should use ImportService with FDO to extract attributes
        var importedAttributes = new[] { "FeatId", "Contractor", "Address", "CreationDate", "LastUpdate" };

        SourceAttributes.Clear();
        foreach (var attr in importedAttributes)
        {
            SourceAttributes.Add(attr);
        }

        // After importing, you can try auto-mapping
        TryAutoMap();
    }

    // Automatic mapping: if names match, set the connection
    private void TryAutoMap()
    {
        // Perform auto-mapping only if both schema and source attributes are loaded
        if (string.IsNullOrEmpty(SelectedSchemaPath) || string.IsNullOrEmpty(SelectedFilePath))
            return;

        foreach (var mapping in MappingItems)
        {
            // If mapping is already set, skip
            if (!string.IsNullOrEmpty(mapping.SelectedSourceAttribute))
                continue;

            // Look for an attribute in the source attribute list with a matching name (case-insensitive)
            var match = SourceAttributes.FirstOrDefault(attr =>
                string.Equals(attr, mapping.TargetAttributeName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(match))
            {
                mapping.SelectedSourceAttribute = match;
            }
            // If no match is found, the value remains empty,
            // and the user will be able to select the desired option from the dropdown list
        }
    }

    // Example command to start conversion
    private bool CanStartConversion(object? parameter) =>
        !string.IsNullOrEmpty(SelectedSchemaPath) &&
        !string.IsNullOrEmpty(SelectedFilePath) &&
        MappingItems.Count > 0;

    private void StartConversion(object? parameter)
    {
        // Here should be the logic to start the conversion considering the mapping
    }

    // Example command to edit schema
    private bool CanEditSchema(object? parameter) => true;//MappingItems.Count > 0;

    private void EditSchema(object? parameter)
    {
        // Here is the logic to open the schema editor window
        var view = new SchemaEditor
        {
            // Pass the injected DialogService instance to SchemaViewModel
            DataContext = new SchemaViewModel()
        };
        view.ShowDialog();
    }
}

// Класс для элемента маппинга
public class MappingItem : ViewModelBase
{
    private string? _selectedSourceAttribute;
    public string? TargetAttributeName { get; set; }
    public string? SelectedSourceAttribute
    {
        get => _selectedSourceAttribute;
        set => Set(ref _selectedSourceAttribute, value);
    }
}