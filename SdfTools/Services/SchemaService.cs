﻿using SdfTools.Models;

namespace SdfTools.Services;

public class SchemaService
{
    public DataSchema CurrentSchema { get; private set; } = new DataSchema();

    public void CreateNewSchema(string name)
    {
        CurrentSchema = new DataSchema { Name = name };
    }

    public void AddAttribute(string name, string dataType)
    {
        if (CurrentSchema.Attributes.Any(a => a.Name == name))
            throw new Exception("Атрибут с таким именем уже существует.");

        CurrentSchema.Attributes.Add(new DataAttribute { Name = name, DataType = dataType });
    }

    public void RemoveAttribute(string name)
    {
        var attribute = CurrentSchema.Attributes.FirstOrDefault(a => a.Name == name);
        if (attribute != null)
            CurrentSchema.Attributes.Remove(attribute);
    }

    public bool ValidateSchema(out string message)
    {
        var duplicateNames = CurrentSchema.Attributes.GroupBy(a => a.Name)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key).ToList();

        if (duplicateNames.Any())
        {
            message = $"Дублируются атрибуты: {string.Join(", ", duplicateNames)}";
            return false;
        }

        message = "Схема валидна.";
        return true;
    }
}
