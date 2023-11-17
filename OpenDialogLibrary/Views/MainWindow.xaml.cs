using System.Windows;

namespace OpenDialogLibrary.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow : Window
    {
        internal MainWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModelLocator.MainWindowVm;
        }
    }
}
