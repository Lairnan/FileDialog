using System.Windows.Controls;

namespace OpenDialogLibrary.Services.Interface
{
    public interface IPageService
    {
        event Action<Page> PageChanged;
        bool CanGoBack { get; }

        void Navigate(Page page);
        void GoBack();
    }
}
