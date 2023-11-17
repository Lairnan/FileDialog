using System.Windows.Controls;

namespace OpenDialogLibrary.Views.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    internal partial class MainPage : Page
    {
        internal MainPage()
        {
            InitializeComponent();
            this.DataContext = ViewModelLocator.MainPageVm;
        }
    }
}
