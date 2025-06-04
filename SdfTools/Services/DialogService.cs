using System.Windows; // Required for MessageBox
using Microsoft.Win32; // Required for OpenFileDialog and SaveFileDialog

namespace SdfTools.Services
{
    public class DialogService : IDialogService
    {
        public void ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowWarning(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowError(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public string? ShowOpenFileDialog(string filter, string? initialDirectory = null)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = filter
            };

            if (initialDirectory != null)
            {
                openFileDialog.InitialDirectory = initialDirectory;
            }

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        public string? ShowSaveFileDialog(string filter, string? initialDirectory = null, string? defaultFileName = null)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = filter
            };

            if (initialDirectory != null)
            {
                saveFileDialog.InitialDirectory = initialDirectory;
            }
            if (defaultFileName != null)
            {
                saveFileDialog.FileName = defaultFileName;
            }

            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;
            }
            return null;
        }
    }
}
