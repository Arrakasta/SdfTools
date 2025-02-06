namespace SdfTools.Models;

public class DataSchema
{
    public string Name { get; set; } = "NewSchema";
    public List<DataAttribute> Attributes { get; set; } = new();
}

public class DataAttribute
{
    public string Name { get; set; }
    public string DataType { get; set; } // Text, Integer, DateTime, Boolean
}
