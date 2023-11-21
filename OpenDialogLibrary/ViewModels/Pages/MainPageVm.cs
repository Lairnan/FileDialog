using System.Collections.ObjectModel;
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
        this.FMainCollection.AddFMains(path);
    }
    
    public ObservableCollection<FMain> FMainCollection
    {
        get => GetProperty<ObservableCollection<FMain>>();
        private init => SetProperty(value, new ObservableCollection<FMain>());
    }
}