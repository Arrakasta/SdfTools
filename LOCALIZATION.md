# Интернационализация SdfTools

## Обзор

Проект SdfTools теперь полностью поддерживает многоязычность с возможностью переключения языка во время выполнения. Реализована поддержка английского и русского языков.

## Структура локализации

### Ресурсные файлы
- `Resources/Strings.resx` - основные ресурсы (английский язык)
- `Resources/Strings.ru.resx` - русские переводы
- `Resources/Strings.Designer.cs` - автогенерированный код для доступа к ресурсам

### Классы локализации
- `Localization/LocalizedStrings.cs` - основной класс для работы с локализованными строками
- `Localization/LocalizationConverter.cs` - конвертер для WPF привязок
- `Localization/LocalizeExtension.cs` - XAML расширение для упрощения использования
- `Services/LocalizationService.cs` - сервис управления языками
- `Converters/CultureDisplayNameConverter.cs` - конвертер для отображения названий языков

## Переведенные строки

### Главное окно (MainWindow)
| Ключ | Английский | Русский |
|------|------------|---------|
| MainWindow_Title | GeoDataConverter | Конвертер геоданных |
| MainWindow_Language | Language: | Язык: |
| MainWindow_SelectedSchema | Selected Schema: | Выбранная схема: |
| MainWindow_SelectSchemaButton | Select Schema | Выбрать схему |
| MainWindow_SourceFile | Source File: | Исходный файл: |
| MainWindow_SelectFileButton | Select File | Выбрать файл |
| MainWindow_AttributeMapping | Attribute Mapping | Маппинг атрибутов |
| MainWindow_TargetAttribute | Target Attribute | Целевой атрибут |
| MainWindow_SourceAttribute | Source Attribute | Исходный атрибут |
| MainWindow_StartConversion | Start Conversion | Начать конвертацию |
| MainWindow_EditSchema | Edit Schema | Редактировать схему |

### Редактор схемы (SchemaEditor)
| Ключ | Английский | Русский |
|------|------------|---------|
| SchemaEditor_Title | Schema Editor | Редактор схемы |
| SchemaEditor_SchemaName | Schema Name: | Имя схемы: |
| SchemaEditor_AddAttribute | Add Attribute: | Добавить атрибут: |
| SchemaEditor_AddButton | Add | Добавить |
| SchemaEditor_DeleteButton | Delete | Удалить |
| SchemaEditor_ValidateSchema | Validate Schema | Проверить схему |

### Диалоги файлов
| Ключ | Английский | Русский |
|------|------------|---------|
| FileDialog_SchemaFilter | Schema Files (*.xml;*.json)\|*.xml;*.json\|All Files (*.*)\|*.* | Файлы схем (*.xml;*.json)\|*.xml;*.json\|Все файлы (*.*)\|*.* |
| FileDialog_GeoDataFilter | Geo Data Files (*.shp;*.sdf;*.sqlite)\|*.shp;*.sdf;*.sqlite | Файлы геоданных (*.shp;*.sdf;*.sqlite)\|*.shp;*.sdf;*.sqlite |

### Сообщения сервиса схемы
| Ключ | Английский | Русский |
|------|------------|---------|
| SchemaService_AttributeAlreadyExists | An attribute with this name already exists. | Атрибут с таким именем уже существует. |
| SchemaService_DuplicateAttributes | Duplicate attributes: {0} | Дублируются атрибуты: {0} |
| SchemaService_SchemaValid | Schema is valid. | Схема валидна. |

### Значения по умолчанию
| Ключ | Английский | Русский |
|------|------------|---------|
| Default_NewSchemaName | NewSchema | НоваяСхема |

## Использование в XAML

### Привязка к локализованным строкам
```xaml
<TextBlock Text="{Binding Source={x:Static loc:LocalizedStrings.Instance}, Path=MainWindow_Title}"/>
```

### С использованием расширения LocalizeExtension
```xaml
<TextBlock Text="{loc:Localize MainWindow_Title}"/>
```

### Для заголовков окон
```xaml
<Window Title="{Binding Source={x:Static loc:LocalizedStrings.Instance}, Path=MainWindow_Title}">
```

## Использование в C# коде

### Получение локализованной строки
```csharp
string title = LocalizedStrings.Instance.MainWindow_Title;
```

### Переключение языка
```csharp
// Переключение на русский
LocalizationService.Instance.SetLanguage("ru");

// Переключение на английский
LocalizationService.Instance.SetLanguage("en");
```

## Добавление новых строк

1. Добавьте новую строку в `Resources/Strings.resx`
2. Добавьте перевод в `Resources/Strings.ru.resx`
3. Добавьте свойство в `LocalizedStrings.cs`:
```csharp
public string MyNewString => GetString(nameof(MyNewString));
```

## Добавление нового языка

1. Создайте новый файл ресурсов: `Resources/Strings.{culture}.resx`
2. Добавьте культуру в `LocalizationService.cs`:
```csharp
_supportedCultures = new Dictionary<string, CultureInfo>
{
    { "en", new CultureInfo("en-US") },
    { "ru", new CultureInfo("ru-RU") },
    { "de", new CultureInfo("de-DE") } // новый язык
};
```
3. Обновите `GetCultureDisplayName` для нового языка

## Особенности реализации

- **Динамическое переключение**: Язык можно менять во время выполнения
- **Design-time поддержка**: Локализация работает в дизайнере Visual Studio
- **Fallback**: При отсутствии перевода используется ключ ресурса
- **Type-safe**: Строгая типизация через свойства класса
- **Performance**: Ленивая инициализация и кэширование ResourceManager
