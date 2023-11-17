using MvvmBuilder.Notifies;
using OpenDialogLibrary.Services.Interface;

namespace OpenDialogLibrary.ViewModels.Pages;

internal class MainPageVm : NotifyBase
{
    private readonly IPageService _pageService;
    private readonly OpenDialog? _openDialog;

    internal MainPageVm(IPageService pageService)
    {
        _pageService = pageService;
        _openDialog = OpenDialog.Instance;
    }
}