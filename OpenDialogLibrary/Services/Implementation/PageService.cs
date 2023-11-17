using ModernWpf.Controls;
using OpenDialogLibrary.Services.Interface;

namespace OpenDialogLibrary.Services.Implementation;

internal class PageService : IPageService
{
    private readonly Stack<Page> _history = new();

    public event Action<Page>? PageChanged;
    public bool CanGoBack => _history.Skip(1).Any();

    public void GoBack()
    {
        if (!this.CanGoBack) return;
            
        _history.Pop();
        PageChanged?.Invoke(_history.Peek());
    }

    public void Navigate(Page page)
    {
        if (page == null) throw new ArgumentNullException(nameof(page), "Page could not be empty");

        _history.Push(page);
        PageChanged?.Invoke(page);
    }
}