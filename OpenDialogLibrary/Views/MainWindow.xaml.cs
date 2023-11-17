namespace OpenDialogLibrary.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
internal partial class MainWindow
{
    internal MainWindow()
    {
        InitializeComponent();
        this.DataContext = ViewModelLocator.MainWindowVm;
    }
}