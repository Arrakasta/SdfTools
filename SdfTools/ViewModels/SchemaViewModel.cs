using SdfTools.Models;
using SdfTools.Services;
using SdfTools.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SdfTools.ViewModels;

public class SchemaViewModel : INotifyPropertyChanged
{
    private readonly SchemaService _schemaService = new SchemaService();
    private string _schemaName;
    private string _newAttributeName;
    private string _selectedDataType;

    public string SchemaName
    {
        get => _schemaName;
        set { _schemaName = value; OnPropertyChanged(nameof(SchemaName)); }
    }

    public ObservableCollection<DataAttribute> Attributes { get; set; } = [];

    public List<string> DataTypes { get; } = ["Text", "Integer", "Double", "DateTime", "Boolean", "SelectList", "Lookup"];

    public string NewAttributeName
    {
        get => _newAttributeName;
        set { _newAttributeName = value; OnPropertyChanged(nameof(NewAttributeName)); }
    }

    public string SelectedDataType
    {
        get => _selectedDataType;
        set { _selectedDataType = value; OnPropertyChanged(nameof(SelectedDataType)); }
    }

    public ICommand AddAttributeCommand { get; }
    public ICommand RemoveAttributeCommand { get; }
    public ICommand ValidateSchemaCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public SchemaViewModel()
    {
        AddAttributeCommand = new RelayCommand(AddAttribute);
        RemoveAttributeCommand = new RelayCommand<DataAttribute>(RemoveAttribute);
        ValidateSchemaCommand = new RelayCommand(ValidateSchema);

        SchemaName = _schemaService.CurrentSchema.Name;
        LoadAttributes();
    }

    private void AddAttribute(object parameter)
    {
        try
        {
            _schemaService.AddAttribute(NewAttributeName, SelectedDataType);
            LoadAttributes();
            NewAttributeName = string.Empty;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void RemoveAttribute(DataAttribute? attribute)
    {
        if (attribute == null) return;

        _schemaService.RemoveAttribute(attribute.Name);
        LoadAttributes();
    }

    private void ValidateSchema(object parameter)
    {
        if (_schemaService.ValidateSchema(out string message))
        {
            MessageBox.Show("Схема валидна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
            MessageBox.Show(message, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void LoadAttributes()
    {
        Attributes.Clear();
        foreach (var attr in _schemaService.CurrentSchema.Attributes)
        {
            Attributes.Add(attr);
        }
    }

    protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
