using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SdfTools.Utilities;
using SdfTools.Views;

namespace SdfTools.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    // Пути к выбранным файлам
    private string _selectedSchemaPath;
    public string SelectedSchemaPath
    {
        get => _selectedSchemaPath;
        set
        {
            _selectedSchemaPath = value;
            OnPropertyChanged();
            TryAutoMap();
        }
    }

    private string _selectedFilePath;
    public string SelectedFilePath
    {
        get => _selectedFilePath;
        set
        {
            _selectedFilePath = value;
            OnPropertyChanged();
            TryAutoMap();
        }
    }

    // Коллекция исходных атрибутов, полученных при импорте файла
    public ObservableCollection<string> SourceAttributes { get; set; } = new ObservableCollection<string>();

    // Коллекция маппинга (элементы, где TargetAttributeName – атрибут из схемы, а SelectedSourceAttribute – выбранный источник)
    public ObservableCollection<MappingItem> MappingItems { get; set; } = new ObservableCollection<MappingItem>();

    // Команды
    public ICommand SelectSchemaCommand { get; }
    public ICommand SelectFileCommand { get; }
    public ICommand StartConversionCommand { get; }
    public ICommand EditSchemaCommand { get; }

    public MainViewModel()
    {
        SelectSchemaCommand = new RelayCommand(SelectSchema);
        SelectFileCommand = new RelayCommand(SelectFile);
        StartConversionCommand = new RelayCommand(StartConversion, CanStartConversion);
        EditSchemaCommand = new RelayCommand(EditSchema, CanEditSchema);
    }

    // Команда выбора схемы
    private void SelectSchema(object parameter)
    {
        OpenFileDialog dlg = new OpenFileDialog
        {
            Filter = "Schema Files (*.xml;*.json)|*.xml;*.json|All Files (*.*)|*.*"
        };

        if (dlg.ShowDialog() == true)
        {
            SelectedSchemaPath = dlg.FileName;
            // Здесь можно добавить логику загрузки схемы и заполнения MappingItems.TargetAttributeName
            LoadSchema(SelectedSchemaPath);
        }
    }

    // Команда выбора исходного файла
    private void SelectFile(object parameter)
    {
        OpenFileDialog dlg = new OpenFileDialog
        {
            Filter = "Geo Data Files (*.shp;*.sdf;*.sqlite)|*.shp;*.sdf;*.sqlite"
        };

        if (dlg.ShowDialog() == true)
        {
            SelectedFilePath = dlg.FileName;
            // Здесь можно добавить логику импорта данных с использованием FDO для заполнения SourceAttributes
            ImportSourceAttributes(SelectedFilePath);
        }
    }

    // Имитация загрузки схемы (заполнение списка целевых атрибутов)
    private void LoadSchema(string schemaPath)
    {
        // Пример: загрузка целевой схемы из файла и заполнение MappingItems
        // В реальной реализации схема будет десериализована в список объектов
        var targetAttributes = new[] { "FeatId", "Contractor", "Address", "CreatedDate", "LastUpdate" };

        MappingItems.Clear();
        foreach (var attr in targetAttributes)
        {
            MappingItems.Add(new MappingItem { TargetAttributeName = attr });
        }

        // После загрузки схемы можно попробовать автозаполнение маппинга
        TryAutoMap();
    }

    // Имитация импорта исходных атрибутов из файла
    private void ImportSourceAttributes(string filePath)
    {
        // Пример: получение списка атрибутов из исходного файла
        // В реальной реализации следует использовать ImportService с FDO для извлечения атрибутов
        var importedAttributes = new[] { "FeatId", "Contractor", "Address", "CreationDate", "LastUpdate" };

        SourceAttributes.Clear();
        foreach (var attr in importedAttributes)
        {
            SourceAttributes.Add(attr);
        }

        // После импорта можно попробовать автозаполнение маппинга
        TryAutoMap();
    }

    // Автоматический маппинг: если имена совпадают, устанавливаем связь
    private void TryAutoMap()
    {
        // Выполняем автозаполнение только если загружены и схема, и исходные атрибуты
        if (string.IsNullOrEmpty(SelectedSchemaPath) || string.IsNullOrEmpty(SelectedFilePath))
            return;

        foreach (var mapping in MappingItems)
        {
            // Если маппинг уже установлен, пропускаем
            if (!string.IsNullOrEmpty(mapping.SelectedSourceAttribute))
                continue;

            // Ищем в списке исходных атрибутов атрибут с совпадающим именем (без учета регистра)
            var match = SourceAttributes.FirstOrDefault(attr =>
                string.Equals(attr, mapping.TargetAttributeName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(match))
            {
                mapping.SelectedSourceAttribute = match;
            }
            // Если совпадения не найдено, значение остается пустым,
            // и пользователь сможет выбрать нужный вариант из выпадающего списка
        }
    }

    // Пример команды запуска конвертации
    private bool CanStartConversion(object parameter) =>
        !string.IsNullOrEmpty(SelectedSchemaPath) &&
        !string.IsNullOrEmpty(SelectedFilePath) &&
        MappingItems.Count > 0;

    private void StartConversion(object parameter)
    {
        // Здесь должна быть логика запуска конвертации с учетом маппинга
    }

    // Пример команды редактирования схемы
    private bool CanEditSchema(object parameter) => true;//MappingItems.Count > 0;
    private void EditSchema(object parameter)
    {
        // Здесь логика открытия окна редактирования схемы
        var view = new SchemaEditor
        {
            DataContext = new SchemaViewModel()
        };
        view.ShowDialog();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

// Класс для элемента маппинга
public class MappingItem : INotifyPropertyChanged
{
    private string _selectedSourceAttribute;
    public string TargetAttributeName { get; set; }
    public string SelectedSourceAttribute
    {
        get => _selectedSourceAttribute;
        set
        {
            _selectedSourceAttribute = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}