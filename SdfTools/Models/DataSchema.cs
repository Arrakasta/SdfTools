using SdfTools.Localization;

namespace SdfTools.Models;

public class DataSchema
{
    public string Name { get; set; } = string.Empty;
    public List<DataAttribute> Attributes { get; set; } = new();

    public DataSchema()
    {
        Name = LocalizedStrings.Instance.Default_NewSchemaName;
    }
}

public class DataAttribute
{
    public string Name { get; set; } = string.Empty;
    public string DataType { get; set; } = string.Empty; // Text, Integer, DateTime, Boolean
}
