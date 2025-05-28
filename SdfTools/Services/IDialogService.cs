namespace SdfTools.Services
{
    public interface IDialogService
    {
        void ShowMessage(string message, string title);
        void ShowWarning(string message, string title);
        void ShowError(string message, string title);
    }
}
