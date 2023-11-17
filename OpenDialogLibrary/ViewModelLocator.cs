using OpenDialogLibrary.ViewModels;
using OpenDialogLibrary.ViewModels.Pages;

namespace OpenDialogLibrary
{
    internal static class ViewModelLocator
    {
        internal static MainWindowVm MainWindowVm => IoC.Resolve<MainWindowVm>();
        internal static MainPageVm MainPageVm => IoC.Resolve<MainPageVm>();
    }
}
