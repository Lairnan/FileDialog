using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using MvvmBuilder.Notifies;
using OpenDialogLibrary.BehaviorsFiles;
using OpenDialogLibrary.Models.Interfaces;
using OpenDialogLibrary.Services.Interface;

namespace OpenDialogLibrary.ViewModels.Pages;

internal class MainPageVm : NotifyBase
{
    private readonly IPageService _pageService;
    
    public MainPageVm(IPageService pageService)
    {
        _pageService = pageService;
        if (OpenDialog.Instance == null) return;
        Load();
    }

    private async void Load()
    {
        var path = string.IsNullOrWhiteSpace(OpenDialog.Instance!.StartDirectory)
            ? OpenDialog.Instance.StartDirectory
            : Environment.CurrentDirectory;
        _fMains.GetDefaultFMains(path);
        this.FMainCollection.SortDescriptions.Add(new SortDescription("FType", ListSortDirection.Descending));
    }

    private readonly ObservableCollection<FMain> _fMains = new();
    
    public ICollectionView FMainCollection => CollectionViewSource.GetDefaultView(_fMains);
}