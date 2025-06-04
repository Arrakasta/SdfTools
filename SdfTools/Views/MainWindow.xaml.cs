using SdfTools.ViewModels; // Add this
using System.Windows;

namespace SdfTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Instantiate MainViewModel with DialogService and set as DataContext
            DataContext = new MainViewModel();
        }
    }
}