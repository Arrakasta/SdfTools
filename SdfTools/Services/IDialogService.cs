namespace SdfTools.Services;

public interface IDialogService
{
    void ShowMessage(string message, string title);
    void ShowWarning(string message, string title);
    void ShowError(string message, string title);
    string? ShowOpenFileDialog(string filter, string? initialDirectory = null);
    string? ShowSaveFileDialog(string filter, string? initialDirectory = null, string? defaultFileName = null);
}
